using System;
using System.Text;

namespace XStream {
    public class XStream {
        public string ToXml(object value) {
            StringBuilder stringBuilder = new StringBuilder();
            XWriter writer = new XWriter(stringBuilder);
            MarshallingContext context = new MarshallingContext(writer);
            try {
                context.ConvertOriginal(value);
                return stringBuilder.ToString();
            }
            finally {
                Console.WriteLine(stringBuilder.ToString());
            }
        }

        public object FromXml(string s) {
            XReader reader = new XReader(s);
            UnmarshallingContext context = new UnmarshallingContext(reader);
            return context.ConvertOriginal();
        }
    }
}