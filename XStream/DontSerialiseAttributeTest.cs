using System.Xml.Serialization;
using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class DontSerialiseAttributeTest : ConverterTestCase {
        [Test]
        public void DoesntSerialiseFieldWithAttribute() {
            var o = (ObjectUsingDontSerialiseAttribute) xstream.FromXml(xstream.ToXml(new ObjectUsingDontSerialiseAttribute(10, 20, 30)));
            Assert.AreEqual(10, o.toBeSerialised);
            Assert.AreEqual(null, o.notToBeSerialised);
            Assert.AreEqual(null, o.alsoNotToBeSerialised);
        }

        private class ObjectUsingDontSerialiseAttribute {
            public readonly object toBeSerialised;
            [DontSerialise] public readonly object notToBeSerialised;
            [XmlIgnore] public readonly object alsoNotToBeSerialised;

            private ObjectUsingDontSerialiseAttribute() {}

            public ObjectUsingDontSerialiseAttribute(object toBeSerialised, object notToBeSerialised, object alsoNotToBeSerialised):this() {
                this.toBeSerialised = toBeSerialised;
                this.alsoNotToBeSerialised = alsoNotToBeSerialised;
                this.notToBeSerialised = notToBeSerialised;
            }
        }
    }
}