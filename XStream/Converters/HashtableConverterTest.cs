using System.Collections;
using NUnit.Framework;

namespace XStream.Converters {
    [TestFixture]
    public class HashtableConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsHashtables() {
            Assert.Fail("currently throws StackOverflow");
            Hashtable hashtable = new Hashtable();
            hashtable.Add(1, 1);
            hashtable.Add(2, null);
            hashtable.Add(new object(), GetType());
            hashtable.Add("some key", new Person("person"));
            hashtable.Add(new Hashtable(), 333);
            SerialiseAndDeserialise(hashtable);
        }
    }
}