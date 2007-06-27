using System;

namespace XStream.Converters
{
    public class IntConverter : Converter
    {
        public bool CanConvert(Type type)
        {
            return type.Equals(typeof (int));
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context)
        {
            writer.StartNode("System.Int32");
            writer.SetValue(value.ToString());
            writer.EndNode();
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context)
        {
            return int.Parse(reader.GetValue());
        }
    }
}