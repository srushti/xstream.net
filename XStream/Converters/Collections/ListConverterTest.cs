using System.Collections;
using NUnit.Framework;
using Rhino.Mocks;

namespace XStream.Converters.Collections
{
    [TestFixture]
    public class ListConverterTest : CollectionConverterTestCase
    {
        private ListConverter converter = new ListConverter();
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
            writer.StartNode("list");
            WriteNode(writer, "System.Int32", 1);
            WriteNode(writer, "System.Int32", 2);
            WriteNode(writer, "System.Int32", 3);
            writer.EndNode();
            mocks.ReplayAll();
            ArrayList list = new ArrayList(new int[] {1, 2, 3,});
            converter.ToXml(list, writer, new MarshallingContext(writer));
        }

        [Test]
        public void Unmarshals()
        {
            XStreamReader reader = mocks.CreateMock<XStreamReader>();
            Expect.Call(reader.NoOfChildren()).Return(3);
            reader.MoveDown();
            ReadNode(reader, 10);
            ReadNode(reader, 20);
            ReadNode(reader, 30);
            Expect.Call(reader.MoveNext()).Return(false);
            reader.MoveUp();
            mocks.ReplayAll();
            object deserialisedArray = converter.FromXml(reader, new UnmarshallingContext(reader));
            ListAsserter(new ArrayList(new int[] {10, 20, 30,}), deserialisedArray);
        }

        [Test]
        public void ConvertsList()
        {
            string serialisedList =
                @"<System.Collections.ArrayList>
    <System.Int32>1</System.Int32>
    <System.Int32>20</System.Int32>
    <System.Int32>300</System.Int32>
</System.Collections.ArrayList>";
            SerialiseAssertAndDeserialise(new ArrayList(new int[] { 1, 20, 300, }), serialisedList, CollectionConverterTestCase.ListAsserter);
        }
    }
}