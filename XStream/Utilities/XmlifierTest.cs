using NUnit.Framework;

namespace xstream.Utilities {
    [TestFixture]
    public class XmlifierTest {
        [Test]
        public void HandlesGenerics() {
            Assert.AreEqual("xstream.GenericObject", Xmlifier.UnXmlify(Xmlifier.Xmlify(typeof (GenericObject<int>))));
        }

        [Test]
        public void XmlifiesWithGenerics() {
            Assert.AreEqual("xstream.GenericObject", Xmlifier.Xmlify(typeof (GenericObject<int>)));
        }
    }
}