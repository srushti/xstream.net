using System;
using System.Reflection;

namespace XStream.Converters
{
    internal class ObjectConverter : Converter
    {
        public bool CanConvert(Type type)
        {
            return false;
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context)
        {
            FieldInfo[] fields = value.GetType().GetFields(Constants.BINDINGFlags);
            foreach (FieldInfo field in fields)
            {
                writer.StartNode(field.Name);
                context.ConvertAnother(field.GetValue(value));
                writer.EndNode();
            }
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context)
        {
            throw new NotImplementedException();
        }
    }
}