using System.Collections;
using NUnit.Framework;

namespace xstream.Converters.Collections {
    public abstract class CollectionConverterTestCase : ConverterTestCase {
        public static void ListAsserter(object first, object second) {
            IList firstList = (IList) first;
            IList secondList = (IList) second;
            Assert.AreEqual(firstList.Count, secondList.Count);
            for (int i = 0; i < firstList.Count; i++)
                Assert.AreEqual(firstList[i], secondList[i]);
        }
    }
}