using NUnit.Framework;
using Rhino.Mocks;

namespace XStream.Converters
{
    [TestFixture]
    public class ObjectConverterTest : ConverterTestCase
    {
        private ObjectConverter converter = new ObjectConverter();

        [Test]
        public void Marshals()
        {
            MockRepository mocks = new MockRepository();
            XStreamWriter writer = mocks.CreateMock<XStreamWriter>();
            writer.StartNode("i");
            writer.SetValue(10.ToString());
            writer.EndNode();
            writer.StartNode("array");
            writer.StartNode("System.Int32");
            writer.SetValue(11.ToString());
            writer.EndNode();
            writer.EndNode();
            mocks.ReplayAll();
            converter.ToXml(new ClassForTesting(10, new int[] {11}), writer, new MarshallingContext(writer));
            mocks.VerifyAll();
        }

        [Test]
        public void Unmarshals()
        {
            Assert.Fail("not yet implemented");
        }

        [Test]
        public void ConvertsObject()
        {
            string serialisedObject =
                @"<XStream.Converters.ClassForTesting>
    <i>100</i>
    <array>
        <System.Int32>1</System.Int32>
    </array>
</XStream.Converters.ClassForTesting>";
            ClassForTesting original = new ClassForTesting(100, new int[] {1,});
            SerialiseAssertAndDeserialise(original, serialisedObject, Assert.AreEqual);
        }
    }

    internal class ClassForTesting
    {
        private readonly int i;
        private readonly int[] array;

        public ClassForTesting(int i, int[] array)
        {
            this.i = i;
            this.array = array;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            ClassForTesting classForTesting = obj as ClassForTesting;
            if (classForTesting == null) return false;
            return i == classForTesting.i && Equals(array, classForTesting.array);
        }

        public override int GetHashCode()
        {
            return i + 29*array.GetHashCode();
        }
    }
}