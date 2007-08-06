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
                context.ConvertAnother(field.GetValue(value));
                writer.EndNode();
            }
        }
    }
}