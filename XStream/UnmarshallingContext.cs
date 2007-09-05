using System;
using System.Collections.Generic;
using XStream.Converters;

namespace XStream {
    public class UnmarshallingContext {
        private readonly Dictionary<string, object> alreadyDeserialised = new Dictionary<string, object>();
        private readonly XStreamReader reader;

        internal UnmarshallingContext(XStreamReader reader) {
            this.reader = reader;
        }

        public object ConvertAnother() {
            string nullAttribute = reader.GetAttribute(Attributes.Null);
            if (nullAttribute != null && nullAttribute == "true") return null;
            object result = Find();
            if (result != null) return result;
            Converter converter = ConverterLookup.GetConverter(reader.GetNodeName());
            if (converter == null) return ConvertOriginal();
            return converter.FromXml(reader, this);
        }

        public object ConvertOriginal() {
            string typeName = reader.GetAttribute(Attributes.classType);
            Type type = Type.GetType(typeName);
            if (type == null) throw new ConversionException("Couldn't deserialise from " + typeName);
            Converter converter = ConverterLookup.GetConverter(type);
            if (converter != null) return converter.FromXml(reader, this);
            return new Unmarshaller(reader, this).Unmarshal(type);
        }

        public void StackObject(object value) {
            alreadyDeserialised.Add(reader.CurrentPath, value);
        }

        public object Find() {
            string referencesAttribute = reader.GetAttribute(Attributes.references);
            if (!string.IsNullOrEmpty(referencesAttribute)) return alreadyDeserialised[referencesAttribute];
            return null;
        }
    }
}