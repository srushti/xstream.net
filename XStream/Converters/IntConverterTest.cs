using NUnit.Framework;

namespace XStream.Converters {
    [TestFixture]
    public class IntConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsInt() {
            SerialiseAssertAndDeserialise(100, "<System.Int32>100</System.Int32>");
        }
    }
}