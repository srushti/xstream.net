using System.Collections;

namespace XStream.Converters.Collections {
    internal class HashtableConverter : BaseDictionaryConverter<Hashtable> {
        protected override IDictionary EmptyDictionary(XStreamReader reader) {
            return new Hashtable();
        }
    }
}