using System.Collections.Generic;
using System.Text;

namespace XStream {
    internal class XmlStack {
        private readonly Stack<string> nodes = new Stack<string>();

        public string CurrentPath {
            get {
                StringBuilder currentPath = new StringBuilder();
                string[] array = nodes.ToArray();
                for (int i = array.Length - 1; i >= 0; i--)
                    currentPath.Append("/" + array[i]);
                return currentPath.ToString();
            }
        }

        public void Push(string node) {
            nodes.Push(node);
        }

        public string Pop() {
            return nodes.Pop();
        }
    }
}