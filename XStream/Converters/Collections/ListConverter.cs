using System;
using System.Collections;

namespace XStream.Converters.Collections
{
    public class ListConverter : Converter
    {
        public bool CanConvert(Type type)
        {
            return typeof (IList).IsAssignableFrom(type);
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context)
        {
            IList list = (IList) value;
            writer.StartNode("list");
            foreach (object o in list)
                context.ConvertAnother(o);
            writer.EndNode();
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context)
        {
            IList result = new ArrayList();
            int count = reader.NoOfChildren();
            reader.MoveDown();
            for (int i = 0; i < count; i++)
            {
                result.Add(context.ConvertAnother());
                reader.MoveNext();
            }
            reader.MoveUp();
            return result;
        }
    }
}