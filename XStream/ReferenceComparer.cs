using System.Collections.Generic;

namespace xstream {
    internal class ReferenceComparer : IEqualityComparer<object> {
        bool IEqualityComparer<object>.Equals(object x, object y) {
            return x == y;
        }

        public int GetHashCode(object obj) {
            return obj.GetHashCode();
        }
    }
}