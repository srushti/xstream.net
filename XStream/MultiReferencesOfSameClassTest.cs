using NUnit.Framework;
using xstream.Converters;
using System.Collections.Generic;

namespace xstream
{
    [TestFixture]
    public class MultiReferencesOfSameClassTest : ConverterTestCase {

        [Test]
        public void ShouldSerializeWithClassAttributes() {
            var bar1 = new BarClass { Name = "bar1" };
            
            var testFoo = new FooClassWithMultipleBarReferences {
                TheBestBar = bar1,
                TheOtherBars = new List<BarClass>()
            };
            SerialiseAndDeserialise(testFoo);
        }
    }

    internal class FooClassWithMultipleBarReferences {
        public BarClass TheBestBar { get; set; }

        public IList<BarClass> TheOtherBars { get; set; }

        public bool Equals(FooClassWithMultipleBarReferences barClass) {
            if (ReferenceEquals(null, barClass)) return false;
            if (ReferenceEquals(this, barClass)) return true;
            return Equals(barClass.TheBestBar, TheBestBar) && 
				barClass.TheOtherBars != null &&
				barClass.TheOtherBars.Count == TheOtherBars.Count;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (FooClassWithMultipleBarReferences)) return false;
            return Equals((FooClassWithMultipleBarReferences) obj);
        }

        public override string ToString() {
            return string.Format(@"FooClassWithMultipleBarReferences with 
                                TheBestBar {0} 
                                and OtherBars {1}", TheBestBar, TheOtherBars);
        }

        public override int GetHashCode() {
            unchecked {
                return ((TheBestBar != null ? TheBestBar.GetHashCode() : 0)*397) ^ (TheOtherBars != null ? TheOtherBars.GetHashCode() : 0);
            }
        }
    }

    internal class BarClass {
        public string Name { get; set; }

        public bool Equals(BarClass barClass) {
            if (barClass == null) return false;
            return Equals(Name, barClass.Name);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as BarClass);
        }

        public override int GetHashCode() {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public override string ToString() {
            return string.Format("BarClass with Name {0}", Name);
        }        
    }
}