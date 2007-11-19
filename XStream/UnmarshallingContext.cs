using System;
using System.Collections.Generic;
using xstream.Converters;

namespace xstream {
    public class UnmarshallingContext {
        private readonly Dictionary<string, object> alreadyDeserialised = new Dictionary<string, object>();
        private readonly XStreamReader reader;
        private readonly ConverterLookup converterLookup;
        private readonly Aliases aliases;

        internal UnmarshallingContext(XStreamReader reader, ConverterLookup converterLookup, Aliases aliases) {
            this.reader = reader;
            this.converterLookup = converterLookup;
            this.aliases = aliases;
        }

        public object ConvertAnother() {
            string nullAttribute = reader.GetAttribute(Attributes.Null);
            if (nullAttribute != null && nullAttribute == "true") return null;
            object result = Find();
            if (result != null) return result;
            Converter converter = converterLookup.GetConverter(reader.GetNodeName());
            if (converter == null) return ConvertOriginal();
            return converter.FromXml(reader, this);
        }

        public object ConvertOriginal() {
            string nodeName = reader.GetNodeName();
            Type type = TypeToUse(nodeName);
            Converter converter = converterLookup.GetConverter(type);
            if (converter != null) return converter.FromXml(reader, this);
            return new Unmarshaller(reader, this, new ConverterLookup()).Unmarshal(type);
        }

        private Type TypeToUse(string nodeName) {
            Type type;
            foreach (Alias alias in aliases) {
                if (alias.TryGetType(nodeName, out type))
                    return type;
            }
            string typeName = reader.GetAttribute(Attributes.classType);
            type = Type.GetType(typeName);
            if (type == null) throw new ConversionException("Couldn't deserialise from " + typeName);
            return type;
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