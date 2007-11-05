using System.Text;
using XStream.Converters;

namespace XStream {
    public class XStream {
        private readonly ConverterLookup converterLookup = new ConverterLookup();

        public string ToXml(object value) {
            StringBuilder stringBuilder = new StringBuilder();
            XWriter writer = new XWriter(stringBuilder);
            MarshallingContext context = new MarshallingContext(writer, converterLookup);
            context.ConvertOriginal(value);
            return stringBuilder.ToString();
        }

        public void AddConverter(Converter converter) {
            converterLookup.AddConverter(converter);
        }

        public object FromXml(string s) {
            XReader reader = new XReader(s);
            UnmarshallingContext context = new UnmarshallingContext(reader, converterLookup);
            return context.ConvertOriginal();
        }
    }
}