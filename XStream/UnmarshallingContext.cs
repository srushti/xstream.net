using System;
using System.Reflection;
using XStream.Converters;

namespace XStream {
    public class UnmarshallingContext {
        private readonly XStreamReader reader;

        public UnmarshallingContext(XStreamReader reader) {
            this.reader = reader;
        }

        public object ConvertAnother() {
            return ConverterLookup.GetConverter(reader.GetNodeName()).FromXml(reader, this);
        }

        public Type LookupArrayType() {
            return Type.GetType(reader.PeekType());
        }

        public object ConvertOriginal() {
            return new Unmarshaller(reader, this).Unmarshal();
        }
    }

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
            if(!(converter is ObjectConverter)) return converter.FromXml(reader, context);
            object result = Activator.CreateInstance(type, true);
            int count = reader.NoOfChildren();
            reader.MoveDown();
//            FieldInfo[] fields = type.GetFields(Constants.BINDINGFlags);
            for (int i = 0; i < count; i++) {
                FieldInfo field = type.GetField(reader.GetNodeName(), Constants.BINDINGFlags);
                field.SetValue(result, ConverterLookup.GetConverter(field.FieldType).FromXml(reader, context));
                reader.MoveNext();
            }
//            foreach (FieldInfo field in fields)
//                field.SetValue(result, ConverterLookup.GetConverter(field.FieldType).FromXml(reader, context));
            reader.MoveUp();
            return result;
        }
    }
}