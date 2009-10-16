using NUnit.Framework;

namespace xstream.Converters.Collections {
    [TestFixture]
    public class ArrayConverterTest : CollectionConverterTestCase {
        [Test]
        public void ConvertsArray() {
            SerialiseAndDeserialise(new int[] {10, 20, 30,});
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