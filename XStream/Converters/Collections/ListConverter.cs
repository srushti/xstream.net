using System;
using System.Collections;

namespace xstream.Converters.Collections {
    internal class ListConverter : Converter {
        private const string LIST_TYPE = "list-type";

        public bool CanConvert(Type type) {
            return typeof (ArrayList).Equals(type);
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context) {
            IList list = (IList) value;
            writer.WriteAttribute(LIST_TYPE, value.GetType().FullName);
            foreach (object o in list)
                context.ConvertOriginal(o);
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context) {
            IList result = (IList) DynamicInstanceBuilder.CreateInstance(Type.GetType(reader.GetAttribute(LIST_TYPE)));
            int count = reader.NoOfChildren();
            reader.MoveDown();
            for (int i = 0; i < count; i++) {
                result.Add(context.ConvertAnother());
                reader.MoveNext();
            }
            reader.MoveUp();
            return result;
        }
    }
}