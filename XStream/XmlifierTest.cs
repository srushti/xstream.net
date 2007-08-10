using System;
using NUnit.Framework;

namespace XStream {
    [TestFixture]
    public class XmlifierTest {
        [Test]
        public void XmlifiesArray() {
            AssertXmlifierForType(typeof (int));
            AssertXmlifierForType(typeof (int[]));
            AssertXmlifierForType(typeof (int[][]));
        }

        [Test]
        public void HandlesGenerics() {
            Assert.AreEqual("XStream.GenericObject", Xmlifier.UnXmlify(Xmlifier.XmlifyAndRemoveGenerics(typeof (GenericObject<int>))));
        }

        [Test]
        public void XmlifiesWithGenerics() {
            Assert.AreEqual("XStream.GenericObject", Xmlifier.XmlifyAndRemoveGenerics(typeof (GenericObject<int>)));
        }

        private static void AssertXmlifierForType(Type type) {
            Assert.AreEqual(type, Type.GetType(Xmlifier.UnXmlify(Xmlifier.Xmlify(type))));
        }
    }
}