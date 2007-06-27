using System.IO;
using System.Text;
using System.Xml;

namespace XStream
{
    public interface XStreamWriter
    {
        void StartNode(string name);
        void SetValue(string value);
        void EndNode();
    }

    public class Writer : XStreamWriter
    {
        private readonly XmlWriter textWriter;

        public Writer(StringBuilder stringBuilder)
        {
            textWriter = new XmlTextWriter(new StringWriter(stringBuilder));
        }

        public void StartNode(string name)
        {
            textWriter.WriteStartElement(name);
        }

        public void SetValue(string value)
        {
            textWriter.WriteValue(value);
        }

        public void EndNode()
        {
            textWriter.WriteEndElement();
        }
    }
}