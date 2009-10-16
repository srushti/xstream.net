using System.Collections;

namespace xstream.Converters.Collections {
    internal class HashtableConverter : BaseDictionaryConverter<Hashtable> {
        protected override IDictionary EmptyDictionary(XStreamReader reader) {
            return new Hashtable();
        }
    }
}