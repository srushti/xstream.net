using System;
using xstream.Converters;
using xstream.Utilities;

namespace xstream {
    public class MarshallingContext {
        private readonly AlreadySerialisedDictionary alreadySerialised = new AlreadySerialisedDictionary();
        private readonly XStreamWriter writer;
        private readonly ConverterLookup converterLookup;
        private readonly Aliases aliases;

        internal MarshallingContext(XStreamWriter writer, ConverterLookup converterLookup, Aliases aliases) {
            this.writer = writer;
            this.converterLookup = converterLookup;
            this.aliases = aliases;
        }

        internal void ConvertAnother(object value) {
            Converter converter = converterLookup.GetConverter(value);
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
            foreach (Alias alias in aliases) {
                string nodeAlias;
                if (alias.TryGetAlias(type, out nodeAlias)) {
                    writer.StartNode(nodeAlias);
                    return;
                }
            }
            writer.StartNode(Xmlifier.XmlifyNode(type));
            writer.WriteAttribute(Attributes.classType, type.AssemblyQualifiedName);
        }
    }
}