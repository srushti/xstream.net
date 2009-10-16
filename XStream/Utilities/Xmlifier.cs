using System;
using System.Text;

namespace xstream.Utilities {
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

        public static string XmlifyNode(Type type) {
            StringBuilder typeName = new StringBuilder(S.RemoveFrom(type.Name.Replace("[]", "-array"), "`"));
            Type[] genericArguments = type.GetGenericArguments();
            for (int i = 0; i < genericArguments.Length; i++) {
                Type genericArgument = genericArguments[i];
                typeName.Append((i == 0 ? "Of" : "And") + XmlifyNode(genericArgument));
            }
            return typeName.ToString();
        }
    }
}