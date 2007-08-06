using System.IO;
using System.Xml.XPath;

namespace XStream {
    public interface XStreamReader {
        string GetValue();
        string GetNodeName();
        bool MoveDown();
        bool MoveNext();
        void MoveUp();
        int NoOfChildren();
        string GetAttribute(string attributeName);
    }

    internal class Reader : XStreamReader {
        private readonly XPathNavigator navigator;

        public Reader(string s) {
            navigator = new XPathDocument(new StringReader(s)).CreateNavigator();
            navigator.MoveToChild(XPathNodeType.Element);
        }

        public string GetValue() {
            return navigator.Value;
        }

        public string GetNodeName() {
            return navigator.LocalName;
        }

        public bool MoveDown() {
            return navigator.MoveToFirstChild();
        }

        public bool MoveNext() {
            return navigator.MoveToNext();
        }

        public void MoveUp() {
            navigator.MoveToParent();
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
    }
}