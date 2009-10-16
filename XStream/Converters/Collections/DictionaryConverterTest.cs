using System.Collections.Generic;
using NUnit.Framework;

namespace xstream.Converters.Collections {
    [TestFixture]
    public class DictionaryConverterTest : ConverterTestCase {
        [Test]
        public void ConvertsDictionary() {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "one");
            dictionary.Add(2, "two");
            dictionary.Add(3, "three");
            SerialiseAndDeserialise(dictionary, CompareDictionaries<int, string>);
        }

        private static void CompareDictionaries<T, U>(object first, object second) {
            Dictionary<T, U> firstDictionary = (Dictionary<T, U>) first;
            Dictionary<T, U> secondDictionary = (Dictionary<T, U>) second;
            Assert.AreEqual(firstDictionary.Count, secondDictionary.Count);
            foreach (KeyValuePair<T, U> pair in firstDictionary) Assert.AreEqual(pair.Value, secondDictionary[pair.Key]);
        }

        [Test]
        public void SignsUpOnlyForGenericDictionaries() {
            Assert.AreEqual(true, new DictionaryConverter().CanConvert(new Dictionary<int, string>().GetType()));
            Assert.AreEqual(false, new DictionaryConverter().CanConvert(new Person("").GetType()));
        }
    }
}