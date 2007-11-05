using System;
using System.Collections;

namespace XStream.Converters.Collections {
    internal class HashtableConverter : Converter {
        private const string KEY = "key";
        private const string VALUE = "value";

        public bool CanConvert(Type type) {
            return type.Equals(typeof (Hashtable));
        }

        public void ToXml(object value, XStreamWriter writer, MarshallingContext context) {
            Hashtable hashtable = (Hashtable) value;
            foreach (DictionaryEntry entry in hashtable) {
                writer.StartNode("entry");
                WriteNode(writer, context, KEY, entry.Key);
                WriteNode(writer, context, VALUE, entry.Value);
                writer.EndNode();
            }
        }

        private static void WriteNode(XStreamWriter writer, MarshallingContext context, string node, object value) {
            writer.StartNode(node);
            Type type = value != null ? value.GetType() : typeof (object);
            writer.WriteAttribute(Attributes.classType, type.AssemblyQualifiedName);
            context.ConvertAnother(value);
            writer.EndNode();
        }

        public object FromXml(XStreamReader reader, UnmarshallingContext context) {
            Hashtable result = new Hashtable();
            int count = reader.NoOfChildren();
            reader.MoveDown();
            for (int i = 0; i < count; i++) {
                reader.MoveDown();
                object key = null, value = null;
                GetObject(context, ref key, ref value, reader);
                reader.MoveNext();
                GetObject(context, ref key, ref value, reader);
                result.Add(key, value);
                reader.MoveUp();
                reader.MoveNext();
            }
            return result;
        }

        private static void GetObject(UnmarshallingContext context, ref object key, ref object value,
                                      XStreamReader reader) {
            string nodeName = reader.GetNodeName();
            object o = context.ConvertOriginal();
            if (KEY.Equals(nodeName)) key = o;
            else value = o;
        }
    }
}