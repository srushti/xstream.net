using NUnit.Framework;
using xstream.Converters;

namespace xstream {
    [TestFixture]
    public class AutoPropertiesTest : ConverterTestCase {
        [Test]
        public void HandlesAutoProperties() {
            var objectWithAnAutoProperty = new ClassWithAnAutoProperty {AutoProperty = 10};
            SerialiseAndDeserialise(objectWithAnAutoProperty);
        }

        internal class ClassWithAnAutoProperty {
            public int AutoProperty { get; set; }

            private bool Equals(ClassWithAnAutoProperty other) {
                return other.AutoProperty == AutoProperty;
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (ClassWithAnAutoProperty)) return false;
                return Equals((ClassWithAnAutoProperty) obj);
            }

            public override int GetHashCode() {
                return AutoProperty;
            }
        }
    }
}