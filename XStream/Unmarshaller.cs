using System;
using System.Reflection;
using XStream.Converters;
using XStream.Utilities;

namespace XStream {
    internal class Unmarshaller {
        private readonly XStreamReader reader;
        private readonly UnmarshallingContext context;
        private readonly ConverterLookup converterLookup;

        public Unmarshaller(XStreamReader reader, UnmarshallingContext context, ConverterLookup converterLookup) {
            this.reader = reader;
            this.context = context;
            this.converterLookup = converterLookup;
        }

        internal object Unmarshal(Type type) {
            object result = context.Find();
            if (result != null) return result;
            if (reader.GetAttribute(Attributes.Null) == true.ToString())
                return null;
            result = DynamicInstanceBuilder.CreateInstance(type);
            context.StackObject(result);
            UnmarshalAs(result, type);
            return result;
        }

        private void UnmarshalAs(object result, Type type) {
            if (type.Equals(typeof (object))) return;
            FieldInfo[] fields =
                type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                               BindingFlags.FlattenHierarchy);
            foreach (FieldInfo field in fields) {
                reader.MoveDown(field.Name);
                field.SetValue(result, ConvertField(field.FieldType));
                reader.MoveUp();
            }
            UnmarshalAs(result, type.BaseType);
        }

        private object ConvertField(Type fieldType) {
            string classAttribute = reader.GetAttribute(Attributes.classType);
            if (!string.IsNullOrEmpty(classAttribute)) fieldType = Type.GetType(Xmlifier.UnXmlify(classAttribute));
            Converter converter = converterLookup.GetConverter(fieldType);
            if (converter != null)
                return converter.FromXml(reader, context);
            else
                return Unmarshal(fieldType);
        }
    }
}