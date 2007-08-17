namespace XStream {
    internal class Attributes {
        internal const string AttributeType = "attributeType";
        internal const string numberOfGenericArgs = "numberOfGenericArgs";
        internal const string references = "references";
        internal const string Null = "null";
        internal const string classType = "class";

        internal static string GenericArg(int i) {
            return "genericArg" + i;
        }
    }
}