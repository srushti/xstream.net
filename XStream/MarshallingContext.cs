namespace XStream
{
    public class MarshallingContext
    {
        private readonly XStreamWriter writer;

        public MarshallingContext(XStreamWriter writer)
        {
            this.writer = writer;
        }

        public void ConvertAnother(object value)
        {
            ConverterLookup.GetConverter(value.GetType()).ToXml(value, writer, this);
        }
    }
}