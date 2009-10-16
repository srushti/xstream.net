using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace xstream.Utilities {
    internal class XmlStack {
        private readonly Stack<string> nodes = new Stack<string>();
        private string justPopped = string.Empty;

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
            if (justPopped.Equals(node)) node = node + "[1]";
            Match match = Regex.Match(justPopped, node + @"\[(\d+)\]");
            if (match.Success) node = node + "[" + (int.Parse(match.Result("$1")) + 1) + "]";
            nodes.Push(node);
        }

        public void Pop() {
            justPopped = nodes.Pop();
        }
    }
}