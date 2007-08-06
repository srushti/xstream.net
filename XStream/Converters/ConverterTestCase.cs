using NUnit.Framework;

namespace XStream.Converters {
    public abstract class ConverterTestCase {
        protected XStream xstream = new XStream();

        internal void SerialiseAssertAndDeserialise(object value, string expectedSerialisedObject, AssertEqualsDelegate equalsDelegate) {
            string actualSerialisedObject = xstream.ToXml(value);
            EqualsIgnoreWhitespace(expectedSerialisedObject, actualSerialisedObject);
            equalsDelegate(value, xstream.FromXml(actualSerialisedObject));
        }

        internal void SerialiseAssertAndDeserialise(object value, string expectedSerialisedObject) {
            SerialiseAssertAndDeserialise(value, expectedSerialisedObject, Assert.AreEqual);
        }

        private static void EqualsIgnoreWhitespace(string expected, string actual) {
            Assert.AreEqual(RemoveWhitespace(expected), RemoveWhitespace(actual));
        }

        private static string RemoveWhitespace(string s) {
            return s.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }
    }

    internal delegate void AssertEqualsDelegate(object first, object second);
}