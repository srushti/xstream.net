using System;

namespace XStream.Converters {
    internal class NullConverter : Converter {
        public bool CanConvert(Type type) {
            return false;
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context) {
            writer.WriteAttribute("null", true.ToString());
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context) {
            return null;
        }
    }
}