using System;

namespace xstream.Converters.Collections {
    internal class ArrayConverter : Converter {
        private const string ARRAY_TYPE = "array-type";

        public bool CanConvert(Type type) {
            return type.IsArray;
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context) {
            Array array = (Array) value;
            string typeName = value.GetType().AssemblyQualifiedName;
            int lastIndexOfBrackets = typeName.LastIndexOf("[]");
            string arrayType = string.Concat(typeName.Substring(0, lastIndexOfBrackets), typeName.Substring(lastIndexOfBrackets + 2));
            writer.WriteAttribute(ARRAY_TYPE, arrayType);
            foreach (object o in array)
                context.ConvertOriginal(o);
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context) {
            int count = reader.NoOfChildren();
            Array result = Array.CreateInstance(context.GetTypeFromOtherAssemblies(reader.GetAttribute(ARRAY_TYPE)), count);
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