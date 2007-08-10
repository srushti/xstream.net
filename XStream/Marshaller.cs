using System;
using System.Reflection;

namespace XStream {
    internal class Marshaller {
        private readonly XStreamWriter writer;
        private readonly MarshallingContext context;

        public Marshaller(XStreamWriter writer, MarshallingContext context) {
            this.writer = writer;
            this.context = context;
        }

        public void Marshal(object value) {
            MarshalAs(value, value.GetType());
        }

        private void MarshalAs(object value, Type type) {
            FieldInfo[] fields = type.GetFields(Constants.BINDINGFlags);
            foreach (FieldInfo field in fields) {
                writer.StartNode(field.Name);
                WriteClassNameIfNeedBe(value, field);
                context.ConvertAnother(field.GetValue(value));
                writer.EndNode();
            }
            if (!typeof (object).Equals(type.BaseType)) MarshalAs(value, type.BaseType);
        }

        private void WriteClassNameIfNeedBe(object value, FieldInfo field) {
            object fieldValue = field.GetValue(value);
            if (fieldValue == null) return;
            Type actualType = fieldValue.GetType();
            if (!field.FieldType.Equals(actualType))
                writer.WriteAttribute("class", Xmlifier.Xmlify(actualType));
        }
    }
}