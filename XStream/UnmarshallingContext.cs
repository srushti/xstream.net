using System;
using System.Collections.Generic;
using XStream.Converters;

namespace XStream {
    public class UnmarshallingContext {
        private readonly Dictionary<string, object> alreadyDeserialised = new Dictionary<string, object>();
        private readonly Reader reader;

        internal UnmarshallingContext(Reader reader) {
            this.reader = reader;
        }

        public object ConvertAnother() {
            string nullAttribute = reader.GetAttribute("null");
            if (nullAttribute != null && nullAttribute == "true") return null;
            object result = Find();
            if (result != null) return result;
            Converter converter = ConverterLookup.GetConverter(reader.GetNodeName());
            if (converter == null) return new Unmarshaller(reader, this).Unmarshal();
            return converter.FromXml(reader, this);
        }

        public object ConvertOriginal() {
            return new Unmarshaller(reader, this).Unmarshal();
        }

        public void StackObject(object value) {
            alreadyDeserialised.Add(reader.CurrentPath, value);
        }

        public object Find() {
            string referencesAttribute = reader.GetAttribute("references");
            if (!string.IsNullOrEmpty(referencesAttribute)) return alreadyDeserialised[referencesAttribute];
            return null;
        }
    }
}