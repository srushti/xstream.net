using System;

namespace xstream.Converters {
    internal class EnumConverter : Converter {
        public bool CanConvert(Type type) {
            return type.IsEnum;
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context) {
            writer.WriteAttribute(Attributes.AttributeType, value.GetType().AssemblyQualifiedName);
            writer.SetValue(value.ToString());
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context) {
            return Enum.Parse(Type.GetType(reader.GetAttribute(Attributes.AttributeType)), reader.GetValue());
        }
    }
}