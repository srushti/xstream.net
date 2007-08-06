using NUnit.Framework;

namespace XStream.Converters {
    [TestFixture]
    public class StringConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsInt() {
            SerialiseAssertAndDeserialise("something ddd7984289*((***('/<>", "<System.String>something ddd7984289*((***('/&lt;&gt;</System.String>");
        }
    }
}