using NUnit.Framework;
using Rhino.Mocks;

namespace XStream.Converters.Collections
{
    [TestFixture]
    public class ArrayConverterTest : CollectionConverterTestCase
    {
        private ArrayConverter converter = new ArrayConverter();
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

        [Test]
        public void Marshals()
        {
            XStreamWriter writer = mocks.CreateMock<XStreamWriter>();
            writer.StartNode("System.Int32-array");
            WriteNode(writer, "System.Int32", 10);
            WriteNode(writer, "System.Int32", 20);
            WriteNode(writer, "System.Int32", 30);
            writer.EndNode();
            mocks.ReplayAll();
            converter.ToXml(new int[] {10, 20, 30,}, writer, new MarshallingContext(writer));
        }

        [Test]
        public void Unmarshals()
        {
            XStreamReader reader = mocks.CreateMock<XStreamReader>();
            Expect.Call(reader.NoOfChildren()).Return(3);
            Expect.Call(reader.GetNodeName()).Return("System.Int32-array");
            reader.MoveDown();
            ReadNode(reader, 10);
            ReadNode(reader, 20);
            ReadNode(reader, 30);
            Expect.Call(reader.MoveNext()).Return(false);
            reader.MoveUp();
            mocks.ReplayAll();
            object deserialisedArray = converter.FromXml(reader, new UnmarshallingContext(reader));
            Assert.AreEqual(new int[] {10, 20, 30,}, deserialisedArray);
        }

        [Test]
        public void ConvertsArray()
        {
            string serialisedArray =
                @"<System.Int32-array>
    <System.Int32>10</System.Int32>
    <System.Int32>20</System.Int32>
    <System.Int32>30</System.Int32>
</System.Int32-array>";
            SerialiseAssertAndDeserialise(new int[] { 10, 20, 30, }, serialisedArray, Assert.AreEqual);
        }
    }
}