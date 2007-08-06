using System;

namespace XStream.Converters {
    internal class StringConverter : Converter {
        public bool CanConvert(Type type) {
            return type.Equals(typeof (string));
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context) {
            writer.SetValue(value.ToString());
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context) {
            return reader.GetValue();
        }
    }
}