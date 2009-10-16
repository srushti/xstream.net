using NUnit.Framework;

namespace xstream.Converters {
    [TestFixture]
    public class NullConverterTest : ConverterTestCase {
        [Test]
        public void InternalNulls() {
            SerialiseAndDeserialise(new Person("name"));
        }

        [Test]
        public void OriginalValueNull() {
            SerialiseAndDeserialise(null);
        }
    }
}