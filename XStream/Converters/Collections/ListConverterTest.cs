using System;
using System.Collections;
using NUnit.Framework;

namespace XStream.Converters.Collections {
    [TestFixture]
    public class ListConverterTest : CollectionConverterTestCase {
        [Test]
        public void ConvertsList() {
            string serialisedList =
                @"<ArrayList class=""System.Collections.ArrayList"" list-type=""System.Collections.ArrayList"">
    <Int32 class=""System.Int32"">1</Int32>
    <Int32 class=""System.Int32"">20</Int32>
    <Int32 class=""System.Int32"">300</Int32>
</ArrayList>";
            SerialiseAssertAndDeserialise(new ArrayList(new int[] {1, 20, 300,}), serialisedList, ListAsserter);
        }

        [Test]
        public void HandlesDerivedList() {
            SerialiseAndDeserialise(new DerivedList(10));
        }

        internal class DerivedList : ArrayList, IEquatable<DerivedList> {
            public readonly int i;

            protected DerivedList() {}

            public DerivedList(int i) {
                this.i = i;
                for (int j = 0; j < i; j++) Add(j);
            }

            public bool Equals(DerivedList derivedList) {
                if (derivedList == null) return false;
                return i == derivedList.i && base.Equals(derivedList);
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(this, obj)) return true;
                return Equals(obj as DerivedList);
            }

            public override int GetHashCode() {
                return i;
            }

            public override string ToString() {
                return i + " " + base.ToString();
            }
        }
    }
}