using System.Collections;
using XStream.Converters;

namespace XStream {
    public class MarshallingContext {
        private readonly Stack stack = new Stack();
        private readonly XStreamWriter writer;

        public MarshallingContext(XStreamWriter writer) {
            this.writer = writer;
        }

        public void ConvertAnother(object value) {
            if (stack.Contains(value)) return;
            stack.Push(value);
            ConvertObject(value);
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