using System.IO;
using System.Xml.XPath;
using xstream.Utilities;

namespace xstream {
    public interface XStreamReader {
        string GetValue();
        string GetNodeName();
        bool MoveDown();
        bool MoveNext();
        void MoveUp();
        int NoOfChildren();
        string GetAttribute(string attributeName);
        bool MoveDown(string name);
        string CurrentPath { get; }
    }

    internal class XReader : XStreamReader {
        private readonly XPathNavigator navigator;
        private readonly XmlStack stack = new XmlStack();

        public XReader(string s) {
            navigator = new XPathDocument(new StringReader(s)).CreateNavigator();
            navigator.MoveToChild(XPathNodeType.Element);
            stack.Push(GetNodeName());
        }

        public string CurrentPath {
            get { return stack.CurrentPath; }
        }

        public string GetValue() {
            return navigator.Value;
        }

        public string GetNodeName() {
            return navigator.LocalName;
        }

        public bool MoveDown() {
            bool succeeded = navigator.MoveToFirstChild();
            if (succeeded) stack.Push(GetNodeName());
            return succeeded;
        }

        public bool MoveNext() {
            bool succeeded = navigator.MoveToNext();
            if (succeeded) {
                stack.Pop();
                stack.Push(GetNodeName());
            }
            return succeeded;
        }

        public void MoveUp() {
            if (navigator.MoveToParent())
                stack.Pop();
        }

        public int NoOfChildren() {
            return navigator.SelectChildren(XPathNodeType.Element).Count;
        }

        public string PeekType() {
            XPathNavigator peekingNavigator = navigator.CreateNavigator();
            peekingNavigator.MoveToFirstChild();
            return peekingNavigator.LocalName;
        }

        public string GetAttribute(string attributeName) {
            return navigator.GetAttribute(attributeName, "");
        }

        public bool MoveDown(string name) {
            bool succeeded = navigator.MoveToChild(name, "");
            if (succeeded) stack.Push(GetNodeName());
            return succeeded;
        }
    }
}