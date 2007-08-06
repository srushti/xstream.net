using System;
using NUnit.Framework;
using XStream.Converters;

namespace XStream {
    [TestFixture]
    public class CircularReferenceTest : ConverterTestCase {
        [Test]
        public void HandlesCircularReferences() {
            Person john = new Person("john");
            Person jane = new Person("jane");
            john.likes = jane;
//            jane.likes = john;
            Console.WriteLine(xstream.ToXml(john));
            string serialisedPeople =
                @"<XStream.Person>
    <likes>
        <likes null=""True"" />
        <name>jane</name>
    </likes>
    <name>john</name>
</XStream.Person>";
            SerialiseAssertAndDeserialise(john, serialisedPeople);
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
            return Equals(likes, person.likes) && Equals(name, person.name);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Person);
        }

        public override int GetHashCode() {
            return (likes != null ? likes.GetHashCode() : 0) + 29*(name != null ? name.GetHashCode() : 0);
        }

        public override string ToString() {
            return name + (likes != null ? (" likes " + likes.name) : "");
        }
    }
}