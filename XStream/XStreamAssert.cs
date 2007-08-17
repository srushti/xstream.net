using System;
using System.Collections;
using NUnit.Framework;

namespace XStream {
    public abstract class XStreamAssert {
        public static void AreEqual(object expected, object actual) {
            Array expectedArray = expected as Array, actualArray = actual as Array;
            if (expectedArray != null && actualArray != null) {
                if (expectedArray.Length != actualArray.Length) Assert.AreEqual(expected, actual);
                for (int i = 0; i < expectedArray.Length; i++) AreEqual(expectedArray.GetValue(i), actualArray.GetValue(i));
                return;
            }
            else if (expected is Person && actual is Person) Person.AssertPersons(expected, actual);
            else if (expected is DateTime && actual is DateTime) AssertDateTime(expected, actual);
            else if (expected is Hashtable && actual is Hashtable) AssertHashtable(expected, actual);
            else Assert.AreEqual(expected, actual);
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
    }
}