using System;
using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class CircularReferenceTest : ConverterTestCase {
        [Test]
        public void HandlesCircularReferences() {
            Person clark = new Person("clark");
            Person lois = new Person("lois");
            clark.likes = lois;
            lois.likes = clark;
            string serialisedPeople = @"<Person class=""" + typeof (Person).AssemblyQualifiedName +
                                      @""">
    <likes>
        <likes references=""/Person"" />
        <name>lois</name>
    </likes>
    <name>clark</name>
</Person>";
            SerialiseAssertAndDeserialise(clark, serialisedPeople, Person.AssertPersons);
        }
    }

    internal class Person : IEquatable<Person> {
        public Person likes;
        public string name;

        protected Person() {}

        public Person(string name) {
            this.name = name;
        }

        public bool Equals(Person person) {
            if (person == null) return false;
            return Equals(name, person.name);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Person);
        }

        public override int GetHashCode() {
            return name != null ? name.GetHashCode() : 0;
        }

        public override string ToString() {
            return name + (likes != null ? (" who likes " + likes.name) : "");
        }

        internal static void AssertPersons(object first, object second) {
            Person firstPerson = (Person) first, secondPerson = (Person) second;
            Assert.AreEqual(firstPerson, secondPerson);
            Assert.AreEqual(firstPerson.likes, secondPerson.likes);
            if (firstPerson.likes == null) return;
            Assert.AreEqual(firstPerson.likes.likes, secondPerson.likes.likes);
            Assert.AreEqual(secondPerson, secondPerson.likes.likes);
        }
    }
}