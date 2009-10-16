using NUnit.Framework;

namespace xstream {
    [TestFixture]
    public class ReaderTest {
        private XReader reader;
        private const string xml = @"<Outer><inner1>111</inner1><inner2>222</inner2></Outer>";

        [SetUp]
        public void SetUp() {
            reader = new XReader(xml);
        }

        [Test]
        public void GetsNodeName() {
            Assert.AreEqual("Outer", reader.GetNodeName());
        }

        [Test]
        public void MovesToInnerNode() {
            reader.MoveDown();
            Assert.AreEqual("inner1", reader.GetNodeName());
        }

        [Test]
        public void MovesToNextNode() {
            Assert.AreEqual(2, reader.NoOfChildren());
            reader.MoveDown();
            reader.MoveNext();
            Assert.AreEqual("inner2", reader.GetNodeName());
        }

        [Test]
        public void GetsCurrentPath() {
            Assert.AreEqual("/Outer", reader.CurrentPath);
            reader.MoveDown();
            Assert.AreEqual("/Outer/inner1", reader.CurrentPath);
            reader.MoveNext();
            Assert.AreEqual("/Outer/inner2", reader.CurrentPath);
            reader.MoveUp();
            Assert.AreEqual("/Outer", reader.CurrentPath);
        }
    }
}