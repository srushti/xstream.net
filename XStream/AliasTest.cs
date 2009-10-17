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

    internal class PersonAlias : StandardAlias {
        protected override Type Type {
            get { return typeof (Person); }
        }

        protected override string Alias {
            get { return Type.Name; }
        }
    }
}