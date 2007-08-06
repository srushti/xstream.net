using System;

namespace XStream {
    public class UnmarshallingContext {
        private readonly Reader reader;

        internal UnmarshallingContext(Reader reader) {
            this.reader = reader;
        }

        public object ConvertAnother() {
            string attribute = reader.GetAttribute("null");
            if (attribute == null || attribute == "true") return null;
            return ConverterLookup.GetConverter(reader.GetNodeName()).FromXml(reader, this);
        }

        public Type LookupArrayType() {
            return Type.GetType(reader.PeekType());
        }

        public object ConvertOriginal() {
            return new Unmarshaller(reader, this).Unmarshal();
        }
    }
}