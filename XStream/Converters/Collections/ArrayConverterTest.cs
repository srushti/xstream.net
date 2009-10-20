using System.Collections.Generic;
using NUnit.Framework;

namespace xstream.Converters.Collections {
    [TestFixture]
    public class ArrayConverterTest : CollectionConverterTestCase {
        [Test]
        public void ConvertsArray() {
            SerialiseAndDeserialise(new[] {10, 20, 30,});
            SerialiseAndDeserialise(new object[] {});
        }

        [Test]
        public void FiguresOutRepeatingObjectsEvenThroughArrays() {
            var duplicateObject = new AmbiguousReferenceHolder(new Person("gl"));
            var objects = new object[] {"", duplicateObject, duplicateObject,};
            SerialiseAndDeserialise(objects);
            objects = (object[]) xstream.FromXml(xstream.ToXml(objects));
            Assert.AreSame(objects[1], objects[2]);
        }

        [Test]
        public void ConvertsTwoDimensionalArrays() {
            var ints = new[] {new[] {1, 2, 3}, new[] {4, 5}, new[] {6, 7, 8},};
            SerialiseAndDeserialise(ints);
        }

        [Test]
        public void HandlesEmptyArrays() {
            SerialiseAndDeserialise(new ClassWithArray {Array = new List<int>()});
        }

        internal class ClassWithArray {
            public List<int> Array;

            private bool Equals(ClassWithArray other) {
                return Equals(other.Array.Count, Array.Count);
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (ClassWithArray)) return false;
                return Equals((ClassWithArray) obj);
            }

            public override int GetHashCode() {
                return (Array != null ? Array.GetHashCode() : 0);
            }
        }
    }
}