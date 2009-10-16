using System.Collections;
using NUnit.Framework;

namespace xstream.Converters {
    [TestFixture]
    public class HashtableConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsHashtables() {
            Hashtable hashtable = new Hashtable();
            SetupRandomData(hashtable);
            SerialiseAndDeserialise(hashtable);
        }

        [Test]
        public void ConvertsDerivedHashtables() {
            Hashtable hashtable = new DerivedHashtable(111);
            SetupRandomData(hashtable);
            SerialiseAndDeserialise(hashtable);
        }

        private static void SetupRandomData(IDictionary hashtable) {
            Person clark = new Person("clark");
            Person lois = new Person("lois");
            clark.likes = lois;
            lois.likes = clark;
            hashtable.Add(1, 1);
            hashtable.Add(2, "some value");
            hashtable.Add(2222L, clark);
            hashtable.Add("some key", null);
            hashtable.Add(clark, lois);
        }

        internal class DerivedHashtable : Hashtable {
            public int x;

            private DerivedHashtable() {}

            public DerivedHashtable(int x) : this() {
                this.x = x;
            }
        }
    }
}