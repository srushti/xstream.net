using System.Collections;
using NUnit.Framework;
using XStream.Converters.Collections;

namespace XStream
{
    [TestFixture]
    public class XStreamTest
    {
        private XStream stream = new XStream();

        [Test]
        public void ConvertsInt()
        {
            SerialiseAssertAndDeserialise(100, "<System.Int32>100</System.Int32>", Assert.AreEqual);
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
            SerialiseAssertAndDeserialise(new int[] {10, 20, 30,}, serialisedArray, Assert.AreEqual);
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
            SerialiseAssertAndDeserialise(new ArrayList(new int[] {1, 20, 300,}), serialisedList, CollectionConverterTestCase.ListAsserter);
        }

        [Test]
        public void ConvertsObject()
        {
            string serialisedObject = @"<XStream.ClassForTesting>
    <i>100</i>
    <array>
        <System.Int32>1</System.Int32>
    </array>
</XStream.ClassForTesting>";
            ClassForTesting original = new ClassForTesting(100, new int[] {1,});
            SerialiseAssertAndDeserialise(original, serialisedObject, Assert.AreEqual);
        }

        private void SerialiseAssertAndDeserialise(object value, string expectedSerialisedObject, AssertEqualsDelegate equalsDelegate)
        {
            string actualSerialisedObject = stream.ToXml(value);
            EqualsIgnoreWhitespace(expectedSerialisedObject, actualSerialisedObject);
            equalsDelegate(value, stream.FromXml(actualSerialisedObject));
        }

        private static void EqualsIgnoreWhitespace(string expected, string actual)
        {
            Assert.AreEqual(RemoveWhitespace(expected), RemoveWhitespace(actual));
        }

        private static string RemoveWhitespace(string s)
        {
            return s.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }
    }

    internal delegate void AssertEqualsDelegate(object first, object second);

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