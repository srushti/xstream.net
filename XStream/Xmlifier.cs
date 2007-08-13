using System;

namespace XStream {
    internal class Xmlifier {
        internal static string UnXmlify(string typeName) {
            return typeName.Replace("-array", "[]").Replace("-plus", "+");
        }

        public static string Xmlify(Type type) {
            string intermediate = type.FullName.Replace("+", "-plus").Replace("[]", "");
            intermediate = S.RemoveFrom(intermediate, "`");
            if (type.IsArray) intermediate += "-array";
            return intermediate;
        }
    }
}