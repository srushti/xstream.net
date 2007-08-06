using System.IO;
using System.Text;
using System.Xml;

namespace XStream {
    public interface XStreamWriter {
        void StartNode(string name);
        void SetValue(string value);
        void EndNode();
        void WriteAttribute(string name, string value);
    }

    internal class Writer : XStreamWriter {
        private readonly XmlWriter textWriter;
        private readonly XmlStack stack = new XmlStack();

        public Writer(StringBuilder stringBuilder) {
            textWriter = new XmlTextWriter(new StringWriter(stringBuilder));
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

        public void WriteAttribute(string name, string value) {
            textWriter.WriteStartAttribute(name);
            textWriter.WriteValue(value);
            textWriter.WriteEndAttribute();
        }
    }
}