using System.Reflection;
using NUnit.Framework;

namespace xstream.Converters {
    [TestFixture]
    public class EnumConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsEnums() {
            SerialiseAndDeserialise(BindingFlags.Public);
        }
    }
}