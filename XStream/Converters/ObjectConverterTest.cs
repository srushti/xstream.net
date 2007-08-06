using System.Text;
using NUnit.Framework;

namespace XStream.Converters {
    [TestFixture]
    public class ObjectConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsObject() {
            string serialisedObject =
                @"<XStream.Converters.ClassForTesting>
    <i>100</i>
    <array>
        <System.Int32>1</System.Int32>
    </array>
</XStream.Converters.ClassForTesting>";
            ClassForTesting original = new ClassForTesting(100, new int[] {1,});
            SerialiseAssertAndDeserialise(original, serialisedObject);
        }

        [Test]
        public void ConvertsPerson() {
            string serialisedPerson = @"<XStream.Person>
    <likes null=""True""/>
    <name>john</name>
</XStream.Person>";
            Person person = new Person("john");
            SerialiseAssertAndDeserialise(person, serialisedPerson);
        }
    }

    internal class ClassForTesting {
        private readonly int i;
        private readonly int[] array;

        private ClassForTesting() {}

        public ClassForTesting(int i, int[] array) : this() {
            this.i = i;
            this.array = array;
        }

        public override bool Equals(object obj) {
            if (this == obj) return true;
            ClassForTesting classForTesting = obj as ClassForTesting;
            if (classForTesting == null) return false;
            return Equals(classForTesting);
        }

        private bool Equals(ClassForTesting classForTesting) {
            if (array.Length != classForTesting.array.Length) return false;
            for (int index = 0; index < array.Length; index++)
                if (array[index] != classForTesting.array[index]) return false;
            return i == classForTesting.i;
        }

        public override int GetHashCode() {
            return i + 29*array.GetHashCode();
        }

        public override string ToString() {
            StringBuilder arrayString = new StringBuilder();
            foreach (int element in array) arrayString.Append(element.ToString());
            return i + " " + arrayString;
        }
    }
}