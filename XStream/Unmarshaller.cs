using System;
using System.Reflection;
using XStream.Converters;

namespace XStream {
    internal class Unmarshaller {
        private readonly XStreamReader reader;
        private readonly UnmarshallingContext context;

        public Unmarshaller(XStreamReader reader, UnmarshallingContext context) {
            this.reader = reader;
            this.context = context;
        }

        public object Unmarshal() {
            string typeName = reader.GetNodeName();
            Type type = Type.GetType(typeName);
            Converter converter = ConverterLookup.GetConverter(typeName);
            if (converter != null) return converter.FromXml(reader, context);
            return Unmarshal(type);
        }

        private object Unmarshal(Type type) {
            object result = context.Find();
            if (result != null) return result;
            result = DynamicInstanceBuilder.CreateInstance(type);
            context.StackObject(result);
            int count = reader.NoOfChildren();
            if (reader.MoveDown()) {
                for (int i = 0; i < count; i++) {
                    FieldInfo field = type.GetField(reader.GetNodeName(), Constants.BINDINGFlags);
                    field.SetValue(result, ConvertField(field.FieldType));
                    reader.MoveNext();
                }
                reader.MoveUp();
            }
            else {
                if (reader.GetAttribute("null") == true.ToString())
                    return null;
            }
            return result;
        }

        private object ConvertField(Type fieldType) {
            string classAttribute = reader.GetAttribute("class");
            if (!string.IsNullOrEmpty(classAttribute)) fieldType = Type.GetType(classAttribute);
            Converter converter = ConverterLookup.GetConverter(fieldType);
            if (converter != null)
                return converter.FromXml(reader, context);
            else
                return Unmarshal(fieldType);
        }
    }
}