using System;

namespace XStream.Converters.Collections
{
    public class ArrayConverter : Converter
    {
        public bool CanConvert(Type type)
        {
            return type.IsArray;
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context)
        {
            Array array = (Array) value;
            writer.StartNode(value.GetType().FullName.Replace("[]", "") + "-array");
            foreach (object o in array)
                context.ConvertAnother(o);
            writer.EndNode();
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context)
        {
            int count = reader.NoOfChildren();
            Array result = Array.CreateInstance(context.LookupArrayType(), count);
            reader.MoveDown();
            for (int i = 0; i < count; i++)
            {
                result.SetValue(context.ConvertAnother(), i);
                reader.MoveNext();
            }
            reader.MoveUp();
            return result;
        }
    }
}