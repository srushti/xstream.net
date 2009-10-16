using System.IO;
using System.Text;
using System.Xml;
using xstream.Utilities;

namespace xstream {
    public interface XStreamWriter {
        void StartNode(string name);
        void SetValue(string value);
        void EndNode();
        void WriteAttribute(string name, object value);
        string CurrentPath { get; }
    }

    internal class XWriter : XStreamWriter {
        private readonly XmlWriter textWriter;
        private readonly XmlStack stack = new XmlStack();

        public XWriter(StringBuilder stringBuilder) {
            textWriter = new XmlTextWriter(new StringWriter(stringBuilder));
            ((XmlTextWriter) textWriter).Formatting = Formatting.Indented;
        }

        public string CurrentPath {
            get { return stack.CurrentPath; }
        }

        public void StartNode(string name) {
            textWriter.WriteStartElement(name);
            stack.Push(name);
        }

        public void SetValue(string value) {
            textWriter.WriteValue(value);
        }

        public void EndNode() {
            textWriter.WriteEndElement();
            stack.Pop();
        }

        public void WriteAttribute(string name, object value) {
            textWriter.WriteStartAttribute(name);
            textWriter.WriteValue(value.ToString());
            textWriter.WriteEndAttribute();
        }
    }
}