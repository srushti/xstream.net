using System.Collections.Generic;
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
        private readonly Stack<string> nodes = new Stack<string>();

        public Writer(StringBuilder stringBuilder) {
            textWriter = new XmlTextWriter(new StringWriter(stringBuilder));
        }

        public string CurrentPath {
            get {
                StringBuilder currentPath = new StringBuilder();
                foreach (string node in nodes) currentPath.Append("/" + node);
                return currentPath.ToString();
            }
        }

        public void StartNode(string name) {
            textWriter.WriteStartElement(name);
            nodes.Push(name);
        }

        public void SetValue(string value) {
            textWriter.WriteValue(value);
        }

        public void EndNode() {
            textWriter.WriteEndElement();
            nodes.Pop();
        }

        public void WriteAttribute(string name, string value) {
            textWriter.WriteStartAttribute(name);
            textWriter.WriteValue(value);
            textWriter.WriteEndAttribute();
        }
    }
}