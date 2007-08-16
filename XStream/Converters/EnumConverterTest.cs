using System.Reflection;
using NUnit.Framework;

namespace XStream.Converters {
    [TestFixture]
    public class EnumConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsEnums() {
            System.Console.WriteLine(BindingFlags.Public.GetType());
            Assert.Fail("throws StackOverflow");
            SerialiseAndDeserialise(BindingFlags.Public);
        }
    }
}