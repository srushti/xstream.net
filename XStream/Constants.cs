using System.Reflection;
using System.Text.RegularExpressions;

namespace xstream {
    internal class Constants {
        internal static readonly BindingFlags BINDINGFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                             BindingFlags.FlattenHierarchy | BindingFlags.DeclaredOnly;

        internal static readonly Regex AutoPropertyNamePattern = new Regex("<(.*)>k__BackingField", RegexOptions.Compiled);
    }
}