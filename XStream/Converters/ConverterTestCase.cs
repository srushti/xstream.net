using System;
using NUnit.Framework;

namespace xstream.Converters {
    public abstract class ConverterTestCase {
        protected XStream xstream = new XStream();

        internal void SerialiseAssertAndDeserialise(object value, string expectedSerialisedObject, AssertEqualsDelegate equalsDelegate) {
            EqualsIgnoreWhitespace(expectedSerialisedObject, SerialiseAndDeserialise(value, equalsDelegate));
        }

        internal void SerialiseAssertAndDeserialise(object value, string expectedSerialisedObject) {
            SerialiseAssertAndDeserialise(value, expectedSerialisedObject, XStreamAssert.AreEqual);
        }

        internal string SerialiseAndDeserialise(object value) {
            return SerialiseAndDeserialise(value, XStreamAssert.AreEqual);
        }

        internal string SerialiseAndDeserialise(object value, AssertEqualsDelegate equalsDelegate) {
            string actualSerialisedObject = xstream.ToXml(value);
            Console.WriteLine(actualSerialisedObject);
            equalsDelegate(value, xstream.FromXml(actualSerialisedObject));
            return actualSerialisedObject;
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