using NUnit.Framework;

namespace xstream.Utilities {
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

        [Test]
        public void AddsIndexNumberIfArrayElement() {
            stack.Push("aaa");
            stack.Pop();
            stack.Push("aaa");
            Assert.AreEqual("/aaa[1]", stack.CurrentPath);
            stack.Pop();
            stack.Push("aaa");
            Assert.AreEqual("/aaa[2]", stack.CurrentPath);
        }
    }
}