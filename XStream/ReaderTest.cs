using NUnit.Framework;

namespace XStream {
    [TestFixture]
    public class ReaderTest {
        private Reader reader;
        private const string xml = @"<Outer><inner1>111</inner1><inner2>222</inner2></Outer>";

        [SetUp]
        public void SetUp() {
            reader = new Reader(xml);
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
            reader.GetNodeName();
            Assert.AreEqual(2, reader.NoOfChildren());
            reader.MoveDown();
            reader.GetNodeName();
            reader.MoveNext();
            Assert.AreEqual("inner2", reader.GetNodeName());
        }
    }
}