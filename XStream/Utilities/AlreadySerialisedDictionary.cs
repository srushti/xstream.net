using System.Collections.Generic;

namespace xstream.Utilities {
    internal class AlreadySerialisedDictionary {
        private readonly Dictionary<object, string> dictionary = new Dictionary<object, string>(new ReferenceComparer());

        public bool ContainsKey(object value) {
            if (value == null) return false;
            return dictionary.ContainsKey(value);
        }

        public string this[object value] {
            get { return dictionary[value]; }
        }

        public void Add(object value, string path) {
            if (value != null)
                dictionary.Add(value, path);
        }
    }
}