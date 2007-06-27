using NUnit.Framework;

namespace XStream.Converters
{
    public class ConverterTestCase
    {
        protected XStream stream = new XStream();

        internal void SerialiseAssertAndDeserialise(object value, string expectedSerialisedObject, AssertEqualsDelegate equalsDelegate)
        {
            string actualSerialisedObject = stream.ToXml(value);
            EqualsIgnoreWhitespace(expectedSerialisedObject, actualSerialisedObject);
            equalsDelegate(value, stream.FromXml(actualSerialisedObject));
        }

        private static void EqualsIgnoreWhitespace(string expected, string actual)
        {
            Assert.AreEqual(RemoveWhitespace(expected), RemoveWhitespace(actual));
        }

        private static string RemoveWhitespace(string s)
        {
            return s.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }
    }

    internal delegate void AssertEqualsDelegate(object first, object second);
}