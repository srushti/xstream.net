using System.Text;

namespace XStream {
    public class XStream {
        public string ToXml(object value) {
            StringBuilder stringBuilder = new StringBuilder();
            XWriter writer = new XWriter(stringBuilder);
            MarshallingContext context = new MarshallingContext(writer);
            context.ConvertOriginal(value);
            return stringBuilder.ToString();
        }

        public object FromXml(string s) {
            XReader reader = new XReader(s);
            UnmarshallingContext context = new UnmarshallingContext(reader);
            return context.ConvertOriginal();
        }
    }
}