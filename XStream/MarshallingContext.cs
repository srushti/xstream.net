using System.Collections.Generic;
using XStream.Converters;

namespace XStream {
    public class MarshallingContext {
        private readonly Dictionary<object, string> alreadySerialised = new Dictionary<object, string>();
        private readonly Writer writer;

        internal MarshallingContext(Writer writer) {
            this.writer = writer;
        }

        public void ConvertAnother(object value) {
            if (alreadySerialised.ContainsKey(value))
                writer.WriteAttribute("references", alreadySerialised[value]);
            else {
                alreadySerialised.Add(value, writer.CurrentPath);
                ConvertObject(value);
            }
        }

        private void ConvertObject(object value) {
            Converter converter = ConverterLookup.GetConverter(value);
            if (converter != null)
                converter.ToXml(value, writer, this);
            else
                new Marshaller(writer, this).Marshal(value);
        }

        public void ConvertOriginal(object value) {
            StartNode(value);
            ConvertAnother(value);
            writer.EndNode();
        }

        private void StartNode(object value) {
            writer.StartNode(value.GetType().FullName.Replace("[]", "-array"));
        }
    }
}