using System.Reflection;

namespace XStream {
    internal class Constants {
        internal static readonly BindingFlags BINDINGFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                             BindingFlags.Instance |
                                                             BindingFlags.FlattenHierarchy | BindingFlags.DeclaredOnly;
    }
}