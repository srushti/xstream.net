using NUnit.Framework;

namespace XStream.Utilities {
    [TestFixture]
    public class XmlifierTest {
        [Test]
        public void HandlesGenerics() {
            Assert.AreEqual("XStream.GenericObject", Xmlifier.UnXmlify(Xmlifier.Xmlify(typeof (GenericObject<int>))));
        }

        [Test]
        public void XmlifiesWithGenerics() {
            Assert.AreEqual("XStream.GenericObject", Xmlifier.Xmlify(typeof (GenericObject<int>)));
        }
    }
}