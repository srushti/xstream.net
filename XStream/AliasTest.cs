using System;
using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class AliasTest : ConverterTestCase {
        [Test]
        public void AbleToAliasDuringSerialisation() {
            Person person = new Person("me");
            XStreamAssert.Contains(@"class=""xstream.Person", xstream.ToXml(person));
            xstream.AddAlias(new PersonAlias());
            XStreamAssert.DoesntContain(@"class=""xstream.Person", xstream.ToXml(person));
        }

        [Test]
        public void DeserialisesUsingAlias() {
            xstream.AddAlias(new PersonAlias());
            SerialiseAndDeserialise(new Person("me"));
        }
    }

    internal class PersonAlias : Alias {
        private readonly Type type = typeof (Person);

        public bool TryGetType(string alias, out Type typeToReturn) {
            typeToReturn = type;
            return type.Name.Equals(alias);
        }

        public bool TryGetAlias(Type type, out string alias) {
            alias = type.Name;
            return type.Equals(type);
        }
    }
}