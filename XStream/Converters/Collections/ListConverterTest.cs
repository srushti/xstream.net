using System.Collections;
using NUnit.Framework;

namespace XStream.Converters.Collections {
    [TestFixture]
    public class ListConverterTest : CollectionConverterTestCase {
        [Test]
        public void ConvertsList() {
            string serialisedList =
                @"<System.Collections.ArrayList>
    <System.Int32>1</System.Int32>
    <System.Int32>20</System.Int32>
    <System.Int32>300</System.Int32>
</System.Collections.ArrayList>";
            SerialiseAssertAndDeserialise(new ArrayList(new int[] {1, 20, 300,}), serialisedList, ListAsserter);
        }
    }
}