using System;
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
            else Assert.AreEqual(expected, actual);
        }
    }
}