using System;

namespace XStream {
    internal class Xmlifier {
        internal static string Xmlify(Type type) {
            return type.FullName.Replace("[]", "-array").Replace("+", "-plus");
        }

        internal static string UnXmlify(string typeName) {
            return typeName.Replace("-array", "[]").Replace("-plus", "+");
        }

        public static string XmlifyAndRemoveGenerics(Type type) {
            return S.RemoveFrom(Xmlify(type), "`");
        }
    }
}