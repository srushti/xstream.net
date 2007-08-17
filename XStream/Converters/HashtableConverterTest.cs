using System.Collections;
using NUnit.Framework;

namespace XStream.Converters {
    [TestFixture]
    public class HashtableConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsHashtables() {
            Person clark = new Person("clark");
            Person lois = new Person("lois");
            clark.likes = lois;
            lois.likes = clark;
            Hashtable hashtable = new Hashtable();
            hashtable.Add(1, 1);
            hashtable.Add(2, "some value");
            hashtable.Add(2222L, clark);
            hashtable.Add("some key", null);
            hashtable.Add(clark, lois);
            SerialiseAndDeserialise(hashtable);
        }
    }
}