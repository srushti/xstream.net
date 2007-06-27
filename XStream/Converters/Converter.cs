using System;

namespace XStream.Converters
{
    public interface Converter
    {
        void ToXml(object value, XStreamWriter writer, MarshallingContext context);
        object FromXml(XStreamReader reader, UnmarshallingContext context);
        bool CanConvert(Type type);
    }
}