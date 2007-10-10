using NUnit.Framework;
using XStream.Converters;

namespace XStream {
    [TestFixture]
    public class MiscellaneousTest : ConverterTestCase {
        [Test]
        public void DoesntSerialiseConstants() {
            Assert.AreEqual(false, xstream.ToXml(new ObjectWithConstant()).Contains(ObjectWithConstant.constant));
        }

        private class ObjectWithConstant {
            public const string constant = "Some stupid constant";
        }
    }
}