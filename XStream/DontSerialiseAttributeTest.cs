using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class DontSerialiseAttributeTest : ConverterTestCase {
        [Test]
        public void DoesntSerialiseFieldWithAttribute() {
            ObjectUsingDontSerialiseAttribute o =
                (ObjectUsingDontSerialiseAttribute) xstream.FromXml(xstream.ToXml(new ObjectUsingDontSerialiseAttribute(10, 20)));
            Assert.AreEqual(10, o.toBeSerialised);
            Assert.AreEqual(null, o.notToBeSerialised);
        }

        private class ObjectUsingDontSerialiseAttribute {
            public readonly object toBeSerialised = null;
            [DontSerialise]
            public readonly object notToBeSerialised = null;

            private ObjectUsingDontSerialiseAttribute() {}

            public ObjectUsingDontSerialiseAttribute(object toBeSerialised, object notToBeSerialised) {
                this.toBeSerialised = toBeSerialised;
                this.notToBeSerialised = notToBeSerialised;
            }
        }
    }
}