using NUnit.Framework;

namespace XStream {
    [TestFixture]
    public class XmlStackTest {
        private XmlStack stack;

        [SetUp]
        public void SetUp() {
            stack = new XmlStack();
        }

        [Test]
        public void FiguresOutCurrentPath() {
            stack.Push("aaa");
            Assert.AreEqual("/aaa", stack.CurrentPath);
            stack.Push("bbb");
            Assert.AreEqual("/aaa/bbb", stack.CurrentPath);
        }
    }
}