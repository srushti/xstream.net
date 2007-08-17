using System;
using System.Reflection;
using XStream.Converters;
using XStream.Utilities;

namespace XStream {
    internal class Unmarshaller {
        private readonly XStreamReader reader;
        private readonly UnmarshallingContext context;

        public Unmarshaller(XStreamReader reader, UnmarshallingContext context) {
            this.reader = reader;
            this.context = context;
        }

        public object Unmarshal() {
            string typeName = Xmlifier.UnXmlify(reader.GetNodeName());
            string genericArgsAttribute = reader.GetAttribute(Attributes.numberOfGenericArgs);
            Type type;
            if (string.IsNullOrEmpty(genericArgsAttribute))
                type = Type.GetType(typeName);
            else
                type = GetGenericType(typeName, int.Parse(genericArgsAttribute));
            if (type == null) throw new ConversionException("Couldn't deserialise from " + typeName);
            Converter converter = ConverterLookup.GetConverter(type);
            if (converter != null) return converter.FromXml(reader, context);
            return Unmarshal(type);
        }

        private Type GetGenericType(string typeName, int noOfGenericArgs) {
            Type genericType = Type.GetType(S.RemoveFrom(typeName, "[]") + "`" + noOfGenericArgs);
            Type[] genericArgs = new Type[noOfGenericArgs];
            for (int i = 0; i < noOfGenericArgs; i++) genericArgs[i] = Type.GetType(Xmlifier.UnXmlify(reader.GetAttribute(Attributes.GenericArg(i))));
            if (typeName.Contains("[]")) return genericType.MakeArrayType();
            return genericType.MakeGenericType(genericArgs);
        }

        private object Unmarshal(Type type) {
            object result = context.Find();
            if (result != null) return result;
            result = DynamicInstanceBuilder.CreateInstance(type);
            context.StackObject(result);
            int count = reader.NoOfChildren();
            if (reader.MoveDown()) {
                for (int i = 0; i < count; i++) {
                    FieldInfo field = type.GetField(reader.GetNodeName(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    field.SetValue(result, ConvertField(field.FieldType));
                    reader.MoveNext();
                }
                reader.MoveUp();
            }
            else {
                if (reader.GetAttribute(Attributes.Null) == true.ToString())
                    return null;
            }
            return result;
        }

        private object ConvertField(Type fieldType) {
            string classAttribute = reader.GetAttribute(Attributes.classType);
            if (!string.IsNullOrEmpty(classAttribute)) fieldType = Type.GetType(Xmlifier.UnXmlify(classAttribute));
            Converter converter = ConverterLookup.GetConverter(fieldType);
            if (converter != null)
                return converter.FromXml(reader, context);
            else
                return Unmarshal(fieldType);
        }
    }
}