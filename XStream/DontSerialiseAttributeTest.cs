using System.Xml.Serialization;
using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class DontSerialiseAttributeTest : ConverterTestCase {
        [Test]
        public void DoesntSerialiseFieldWithAttribute() {
            ObjectUsingDontSerialiseAttribute o = (ObjectUsingDontSerialiseAttribute) xstream.FromXml(xstream.ToXml(new ObjectUsingDontSerialiseAttribute(10, 20, 30)));
            Assert.AreEqual(10, o.toBeSerialised);
            Assert.AreEqual(null, o.notToBeSerialised);
            Assert.AreEqual(null, o.alsoNotToBeSerialised);
        }

        private class ObjectUsingDontSerialiseAttribute {
#pragma warning disable RedundantDefaultFieldInitializer
            public readonly object toBeSerialised = null;
            [DontSerialise] public readonly object notToBeSerialised = null;
            [XmlIgnore] public readonly object alsoNotToBeSerialised = null;
#pragma warning restore RedundantDefaultFieldInitializer

            private ObjectUsingDontSerialiseAttribute() {}

            public ObjectUsingDontSerialiseAttribute(object toBeSerialised, object notToBeSerialised, object alsoNotToBeSerialised):this() {
                this.toBeSerialised = toBeSerialised;
                this.alsoNotToBeSerialised = alsoNotToBeSerialised;
                this.notToBeSerialised = notToBeSerialised;
            }
        }
    }
}