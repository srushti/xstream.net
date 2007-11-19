using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;

namespace xstream {
    internal class DynamicInstanceBuilder {
        private const string prefix = ".xsnet~";
        private static ModuleBuilder moduleBuilder = null;
        private static readonly Hashtable typeMap = Hashtable.Synchronized(new Hashtable());

        private static ModuleBuilder ModuleBuilder {
            get {
                lock (typeof (DynamicInstanceBuilder)) {
                    if (moduleBuilder == null) {
                        AssemblyName assemblyName = new AssemblyName();
                        assemblyName.Name = ".xsnet";
                        AppDomain domain = AppDomain.CurrentDomain;
                        AssemblyBuilder ab = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                        moduleBuilder = ab.DefineDynamicModule(".xstreamnet.dll");
                    }
                }
                return moduleBuilder;
            }
        }

        private static object GetDynamicInstance(Type type) {
            if (typeof (MulticastDelegate).IsAssignableFrom(type)) {
                DynamicMethod method = new DynamicMethod("XStreamDynamicDelegate", typeof (void), GetDelegateParameterTypes(type), typeof (object));
                ILGenerator generator = method.GetILGenerator();
                generator.Emit(OpCodes.Ret);
                return method.CreateDelegate(type);
            }
            if (type.IsSealed)
                throw new ConversionException("Impossible to construct type: " + type);

            // Check if we already have the type defined
            string typeName = prefix + type;
            lock (typeMap) {
                Type dynamicType = typeMap[typeName] as Type;

                if (dynamicType == null) {
                    TypeBuilder typeBuilder = ModuleBuilder.DefineType(typeName, TypeAttributes.Class | TypeAttributes.NotPublic, type);

                    ConstructorBuilder cb = typeBuilder.DefineConstructor(MethodAttributes.Private, CallingConventions.Standard, new Type[0]);
                    cb.GetILGenerator().Emit(OpCodes.Ret);

                    dynamicType = typeBuilder.CreateType();
                    typeMap[typeName] = dynamicType;
                }

                return Activator.CreateInstance(dynamicType, true);
            }
        }

        private static Type[] GetDelegateParameterTypes(Type d) {
            if (d.BaseType != typeof (MulticastDelegate))
                throw new ApplicationException("Not a delegate.");

            MethodInfo invoke = d.GetMethod("Invoke");
            if (invoke == null)
                throw new ApplicationException("Not a delegate.");

            ParameterInfo[] parameters = invoke.GetParameters();
            Type[] typeParameters = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                typeParameters[i] = parameters[i].ParameterType;
            return typeParameters;
        }

        public static object CreateInstance(Type type) {
            if (type.IsValueType || type.GetConstructor(Constants.BINDINGFlags, null, new Type[0], null) != null)
                return Activator.CreateInstance(type, true);
            else
                return GetDynamicInstance(type);
        }
    }
}