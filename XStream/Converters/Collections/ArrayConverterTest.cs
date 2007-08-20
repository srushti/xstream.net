using NUnit.Framework;

namespace XStream.Converters.Collections {
    [TestFixture]
    public class ArrayConverterTest : CollectionConverterTestCase {
        [Test]
        public void ConvertsArray() {
            string serialisedArray =
                @"<Int32-array class=""System.Int32[]"" array-type=""System.Int32"">
    <Int32 class=""System.Int32"">10</Int32>
    <Int32 class=""System.Int32"">20</Int32>
    <Int32 class=""System.Int32"">30</Int32>
</Int32-array>";
            SerialiseAssertAndDeserialise(new int[] {10, 20, 30,}, serialisedArray);
        }

        [Test]
        public void FiguresOutRepeatingObjectsEvenThroughArrays() {
            AmbiguousReferenceHolder duplicateObject = new AmbiguousReferenceHolder(new Person("gl"));
            object[] objects = new object[] {"", duplicateObject, duplicateObject,};
            SerialiseAndDeserialise(objects);
            objects = (object[]) xstream.FromXml(xstream.ToXml(objects));
            Assert.AreSame(objects[1], objects[2]);
        }

        [Test]
        public void ConvertsTwoDimensionalArrays() {
            int[][] ints = new int[][] {new int[] {1, 2, 3}, new int[] {4, 5}, new int[] {6, 7, 8},};
            SerialiseAndDeserialise(ints);
        }
    }
}