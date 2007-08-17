using System.IO;
using System.Text;
using System.Xml;
using XStream.Utilities;

namespace XStream {
    public interface XStreamWriter {
        void StartNode(string name);
        void SetValue(string value);
        void EndNode();
        void WriteAttribute(string name, object value);
        void StartCollectionNode(string name, int index);
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

        public void StartCollectionNode(string name, int index) {
            textWriter.WriteStartElement(name);
            stack.Push(name + "[" + index + "]");
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