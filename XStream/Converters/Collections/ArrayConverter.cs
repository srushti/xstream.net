using System;

namespace xstream.Converters.Collections {
    internal class ArrayConverter : Converter {
        public bool CanConvert(Type type) {
            return type.IsArray;
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context) {
            Array array = (Array) value;
            string typeName = value.GetType().AssemblyQualifiedName;
            writer.WriteAttribute("array-type", typeName.Substring(0, typeName.LastIndexOf("[]")));
            foreach (object o in array)
                context.ConvertOriginal(o);
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context) {
            int count = reader.NoOfChildren();
            Array result = Array.CreateInstance(context.GetTypeFromOtherAssemblies(reader.GetAttribute("array-type")), count);
            reader.MoveDown();
            for (int i = 0; i < count; i++) {
                result.SetValue(context.ConvertOriginal(), i);
                reader.MoveNext();
            }
            reader.MoveUp();
            return result;
        }
    }
}