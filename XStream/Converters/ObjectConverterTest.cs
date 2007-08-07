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

        [Test]
        public void ConvertsHouse() {
            string serialisedHouse =
                @"<XStream.Converters.House>
    <father>
        <likes>
            <likes references=""/XStream.Converters.House/father""/>
            <name>mom</name>
        </likes>
        <name>dad</name>
    </father>
    <mother references=""/XStream.Converters.House/father/likes""/>
    <child>
        <likes null=""True""/>
        <name>kid</name>
    </child>
</XStream.Converters.House>";
            House house = new House(new Person("dad"), new Person("mom"), new Person("kid"));
            SerialiseAssertAndDeserialise(house, serialisedHouse, House.AssertHouse);
        }

        [Test]
        public void HandlesAmbiguousReferences() {
            string serialisedHolder = @"<XStream.Converters.AmbiguousReferenceHolder>
    <o class=""System.String"">x</o>
</XStream.Converters.AmbiguousReferenceHolder>";
            AmbiguousReferenceHolder holder = new AmbiguousReferenceHolder("x");
            SerialiseAssertAndDeserialise(holder, serialisedHolder, AmbiguousReferenceHolder.AssertHolder);
        }
    }

    internal class House {
        public Person father;
        public Person mother;
        public Person child;

        protected House() {}

        public House(Person father, Person mother, Person child) {
            this.father = father;
            this.mother = mother;
            this.child = child;
            father.likes = mother;
            mother.likes = father;
        }

        public static void AssertHouse(object first, object second) {
            House firstHouse = (House) first, secondHouse = (House) second;
            Person.AssertPersons(firstHouse.father, secondHouse.father);
            Person.AssertPersons(firstHouse.mother, secondHouse.mother);
            Person.AssertPersons(firstHouse.child, secondHouse.child);
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

    internal class AmbiguousReferenceHolder {
        public object o;

        protected AmbiguousReferenceHolder() {}

        public AmbiguousReferenceHolder(object o) {
            this.o = o;
        }

        public static void AssertHolder(object x, object y) {
            AmbiguousReferenceHolder first = (AmbiguousReferenceHolder) x, second = (AmbiguousReferenceHolder) y;
            Assert.AreEqual(first.o, second.o);
        }
    }
}