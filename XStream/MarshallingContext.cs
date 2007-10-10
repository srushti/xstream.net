using System;
using XStream.Converters;
using XStream.Utilities;

namespace XStream {
    public class MarshallingContext {
        private readonly AlreadySerialisedDictionary alreadySerialised = new AlreadySerialisedDictionary();
        private readonly XStreamWriter writer;

        internal MarshallingContext(XStreamWriter writer) {
            this.writer = writer;
        }

        internal void ConvertAnother(object value) {
            Converter converter = ConverterLookup.GetConverter(value);
            if (converter != null) converter.ToXml(value, writer, this);
            else ConvertObject(value);
        }

        private void ConvertObject(object value) {
            if (alreadySerialised.ContainsKey(value))
                writer.WriteAttribute(Attributes.references, alreadySerialised[value]);
            else {
                alreadySerialised.Add(value, writer.CurrentPath);
                new Marshaller(writer, this).Marshal(value);
            }
        }

        public void ConvertOriginal(object value) {
            StartNode(value);
            ConvertAnother(value);
            writer.EndNode();
        }

        private void StartNode(object value) {
            Type type = value != null ? value.GetType() : typeof (object);
            writer.StartNode(Xmlifier.XmlifyNode(type));
            writer.WriteAttribute(Attributes.classType, type.AssemblyQualifiedName);
        }
    }
}