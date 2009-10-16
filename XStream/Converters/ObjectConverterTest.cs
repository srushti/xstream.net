using System;
using System.Text;
using NUnit.Framework;

namespace xstream.Converters {
    [TestFixture]
    public class ObjectConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsObject() {
            SerialiseAndDeserialise(new ClassForTesting(100, new int[] {1,}));
        }

        [Test]
        public void ConvertsPerson() {
            SerialiseAndDeserialise(new Person("john"));
        }

        [Test]
        public void ConvertsHouse() {
            House house = new House(new Person("dad"), new Person("mom"), new Person("kid"));
            SerialiseAndDeserialise(house, House.AssertHouse);
        }

        [Test]
        public void HandlesAmbiguousReferences() {
            SerialiseAndDeserialise(new AmbiguousReferenceHolder("x"), AmbiguousReferenceHolder.AssertHolder);
            SerialiseAndDeserialise(new AmbiguousReferenceHolder(new string[] {"1", "2"}), AmbiguousReferenceHolder.AssertHolder);
        }

        [Test]
        public void WorksWithArraysHoldingDerivedTypes() {
            SerialiseAndDeserialise(new object[] {1, 2, "222", new AmbiguousReferenceHolder(new string[] {})}, XStreamAssert.AreEqual);
        }

        [Test]
        public void HandlesPrivateFieldsOfBaseClassWithSameName() {
            SerialiseAndDeserialise(new DerivedObject());
        }

        [Test]
        public void DoesntConvertDelegates() {
            SerialiseAndDeserialise(new ObjectWithEvent());
        }
    }

    internal class DerivedObject : BaseObject {
        public readonly int i = 100;

        public bool Equals(DerivedObject derivedObject) {
            if (derivedObject == null) return false;
            if (!base.Equals(derivedObject)) return false;
            return i == derivedObject.i;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as DerivedObject);
        }

        public override int GetHashCode() {
            return base.GetHashCode() + 29*i;
        }

        public override string ToString() {
            return "derived i = " + i + " " + base.ToString();
        }
    }

    internal class BaseObject : IEquatable<BaseObject> {
        private readonly int i = 10;

        public bool Equals(BaseObject baseObject) {
            if (baseObject == null) return false;
            return i == baseObject.i;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as BaseObject);
        }

        public override int GetHashCode() {
            return i;
        }

        public override string ToString() {
            return "base i = " + i;
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

    internal class AmbiguousReferenceHolder : IEquatable<AmbiguousReferenceHolder> {
        public object o;

        protected AmbiguousReferenceHolder() {}

        public AmbiguousReferenceHolder(object o) {
            this.o = o;
        }

        public bool Equals(AmbiguousReferenceHolder ambiguousReferenceHolder) {
            if (ambiguousReferenceHolder == null) return false;
            AmbiguousReferenceHolder first = this, second = ambiguousReferenceHolder;
            if (first.o.GetType().IsArray) {
                Array thisArray = (Array) first.o;
                Array otherArray = (Array) second.o;
                for (int i = 0; i < thisArray.Length; i++)
                    if (!thisArray.GetValue(i).Equals(otherArray.GetValue(i))) return false;
                return true;
            }
            return o.Equals(ambiguousReferenceHolder.o);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as AmbiguousReferenceHolder);
        }

        public override int GetHashCode() {
            return o != null ? o.GetHashCode() : 0;
        }

        public static void AssertHolder(object x, object y) {}

        public override string ToString() {
            if (o == null) return "";
            if (o.GetType().IsArray) {
                StringBuilder builder = new StringBuilder("ambiguousreferenceholder with array:");
                for (int i = 0; i < ((Array) o).Length; i++)
                    builder.Append(i + " " + ((Array) o).GetValue(i) + " ");
                return builder.ToString();
            }
            return "ambiguousreferenceholder with " + o;
        }
    }

    internal class ObjectWithEvent : IEquatable<ObjectWithEvent> {
        internal event VoidDelegate SomeEvent = delegate { };

        internal delegate void VoidDelegate();

        public bool Equals(ObjectWithEvent objectWithEvent) {
            return objectWithEvent != null;
        }

        public override bool Equals(object obj) {
            return ReferenceEquals(this, obj) || Equals(obj as ObjectWithEvent);
        }

        public override int GetHashCode() {
            return 0;
        }
    }
}