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
            FieldInfo[] fields = value.GetType().GetFields(Constants.BINDINGFlags);
            foreach (FieldInfo field in fields) {
                writer.StartNode(field.Name);
                WriteClassNameIfNeedBe(value, field);
                context.ConvertAnother(field.GetValue(value));
                writer.EndNode();
            }
        }

        private void WriteClassNameIfNeedBe(object value, FieldInfo field) {
            object fieldValue = field.GetValue(value);
            if (fieldValue == null) return;
            Type actualType = fieldValue.GetType();
            if (!field.FieldType.Equals(actualType))
                writer.WriteAttribute("class", actualType.FullName.Replace("[]", "-array"));
        }
    }
}