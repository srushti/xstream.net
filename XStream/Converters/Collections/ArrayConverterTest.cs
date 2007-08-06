using NUnit.Framework;

namespace XStream.Converters.Collections {
    [TestFixture]
    public class ArrayConverterTest : CollectionConverterTestCase {
        [Test]
        public void ConvertsArray() {
            string serialisedArray =
                @"<System.Int32-array>
    <System.Int32>10</System.Int32>
    <System.Int32>20</System.Int32>
    <System.Int32>30</System.Int32>
</System.Int32-array>";
            SerialiseAssertAndDeserialise(new int[] {10, 20, 30,}, serialisedArray);
        }
    }
}