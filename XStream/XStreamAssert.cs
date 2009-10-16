using System;
using System.Collections;
using NUnit.Framework;
using xstream.Converters;
using xstream.Converters.Collections;

namespace xstream {
    public abstract class XStreamAssert {
        public static void AreEqual(object expected, object actual) {
            Array expectedArray = expected as Array, actualArray = actual as Array;
            if (expectedArray != null && actualArray != null) {
                if (expectedArray.Length != actualArray.Length) Assert.AreEqual(expected, actual);
                for (int i = 0; i < expectedArray.Length; i++)
                    AreEqual(expectedArray.GetValue(i), actualArray.GetValue(i));
                return;
            }
            else if (expected is Person && actual is Person) Person.AssertPersons(expected, actual);
            else if (expected is DateTime && actual is DateTime) AssertDateTime(expected, actual);
            else if (expected is HashtableConverterTest.DerivedHashtable && actual is HashtableConverterTest.DerivedHashtable)
                AssertDerivedHashtable(expected, actual);
            else if (expected is Hashtable && actual is Hashtable) AssertHashtable(expected, actual);
            else if (expected is ListConverterTest.DerivedList && actual is ListConverterTest.DerivedList)
                AssertDerivedList(expected, actual);
            else Assert.AreEqual(expected, actual);
        }

        private static void AssertDerivedHashtable(object expected, object actual) {
            HashtableConverterTest.DerivedHashtable expectedHashtable = (HashtableConverterTest.DerivedHashtable) expected,
                                                    actualHashtable = (HashtableConverterTest.DerivedHashtable) actual;
            Assert.AreEqual(expectedHashtable.x, actualHashtable.x);
            AssertHashtable(expectedHashtable, actualHashtable);
        }

        private static void AssertDerivedList(object expected, object actual) {
            ListConverterTest.DerivedList expectedList = (ListConverterTest.DerivedList) expected, actualList = (ListConverterTest.DerivedList) actual;
            Assert.AreEqual(expectedList.i, actualList.i);
            Assert.AreEqual(expectedList.Count, actualList.Count);
            for (int i = 0; i < actualList.Count; i++) AreEqual(expectedList[i], actualList[i]);
        }

        private static void AssertHashtable(object expected, object actual) {
            Hashtable expectedHashtable = (Hashtable) expected, actualHashtable = (Hashtable) actual;
            Assert.AreEqual(expectedHashtable.Count, actualHashtable.Count);
            foreach (DictionaryEntry entry in expectedHashtable) {
                Assert.AreEqual(true, actualHashtable.ContainsKey(entry.Key), "doesn't contain " + entry.Key);
                AreEqual(entry.Value, actualHashtable[entry.Key]);
            }
        }

        private static void AssertDateTime(object expected, object actual) {
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        public static void Contains(string expectedSubstring, string actual) {
            Contains(true, actual, expectedSubstring, "doesn't contain");
        }

        public static void DoesntContain(string expectedSubstring, string actual) {
            Contains(false, actual, expectedSubstring, "contains");
        }

        private static void Contains(bool expected, string actual, string expectedSubstring, string subMessage) {
            Assert.AreEqual(expected, actual.Contains(expectedSubstring), actual + "\n" + subMessage + "\n" + expectedSubstring);
        }
    }
}