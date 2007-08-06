using System.Text;

namespace XStream {
    public class XStream {
        public string ToXml(object value) {
            StringBuilder stringBuilder = new StringBuilder();
            Writer writer = new Writer(stringBuilder);
            MarshallingContext context = new MarshallingContext(writer);
            context.ConvertOriginal(value);
            return stringBuilder.ToString();
        }

        public object FromXml(string s) {
            Reader reader = new Reader(s);
            UnmarshallingContext context = new UnmarshallingContext(reader);
            return context.ConvertOriginal();
        }
    }
}