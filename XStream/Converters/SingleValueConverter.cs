using System;

namespace XStream.Converters {
    internal class SingleValueConverter<T> : Converter {
        private readonly Parse<T> parse;

        public SingleValueConverter(Parse<T> parse) {
            this.parse = parse;
        }

        protected static Type ConvertingType() {
            return typeof (T);
        }

        public bool CanConvert(Type type) {
            return type.Equals(ConvertingType());
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context) {
            writer.SetValue(value.ToString());
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context) {
            return parse(reader.GetValue());
        }
    }

    internal delegate T Parse<T>(string s);
}