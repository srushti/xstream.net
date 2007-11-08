using NUnit.Framework;
using xstream.Converters;
using xstream.Converters;

namespace xstream {
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