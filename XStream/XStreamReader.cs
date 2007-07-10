using System.IO;
using System.Xml.XPath;

namespace XStream {
    public interface XStreamReader {
        string GetValue();
        string GetNodeName();
        void MoveDown();
        bool MoveNext();
        void MoveUp();
        int NoOfChildren();
        string PeekType();
    }

    internal class Reader : XStreamReader {
        private XPathNavigator navigator;

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

        public void MoveDown() {
            navigator.MoveToFirstChild();
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
    }
}