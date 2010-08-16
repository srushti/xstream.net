using System.Xml.Serialization;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using xstream.Converters;

namespace xstream {
	[TestFixture]
	public class ConverterLookupTest {
		[Test]
		public void UserDefinedConvertersHavePriority () {
			var lookup = new ConverterLookup ();
			var alternateConverter = new AlternateStringConverter ();
			lookup.AddConverter (alternateConverter);
			Assert.That (lookup.GetConverter (typeof(string)), Is.EqualTo (alternateConverter));
		}

		private class AlternateStringConverter : Converter {
			public bool CanConvert (System.Type type) {
				return type.Equals(typeof(string));
			}


			public void ToXml (object value, XStreamWriter writer, MarshallingContext context) {
				throw new System.NotImplementedException ();
			}


			public object FromXml (XStreamReader reader, UnmarshallingContext context) {
				throw new System.NotImplementedException ();
			}
			
		}
	}
}
