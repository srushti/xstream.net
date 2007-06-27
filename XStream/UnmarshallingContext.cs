using System;

namespace XStream
{
    public class UnmarshallingContext
    {
        private readonly XStreamReader reader;

        public UnmarshallingContext(XStreamReader reader)
        {
            this.reader = reader;
        }

        public object ConvertAnother()
        {
            return ConverterLookup.GetConverter(reader.GetNodeName()).FromXml(reader, this);
        }

        public Type LookupArrayType()
        {
            return Type.GetType(reader.GetNodeName().Replace("-array", ""));
        }
    }
}