using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class MiscellaneousTest : ConverterTestCase {
        [Test]
        public void DoesntSerialiseConstants() {
            Assert.AreEqual(false, xstream.ToXml(new ObjectWithConstantAndStatic()).Contains(ObjectWithConstantAndStatic.constant));
        }

        [Test]
        public void DoesntSerialiseStatics() {
            Assert.AreEqual(false, xstream.ToXml(new ObjectWithConstantAndStatic()).Contains(ObjectWithConstantAndStatic.stat));
        }

        private class ObjectWithConstantAndStatic {
            public const string constant = "Some stupid constant";
            public static readonly string stat = "Some stupid static";
        }
    }
}