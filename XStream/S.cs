namespace xstream {
    internal class S {
        public static string RemoveFrom(string original, string from) {
            int index = original.IndexOf(from);
            if (index < 0) return original;
            return original.Remove(index);
        }
    }
}