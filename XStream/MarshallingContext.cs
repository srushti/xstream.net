using System;
using XStream.Converters;

namespace XStream {
    public class MarshallingContext {
        private readonly AlreadySerialisedDictionary alreadySerialised = new AlreadySerialisedDictionary();
        private readonly Writer writer;

        internal MarshallingContext(Writer writer) {
            this.writer = writer;
        }

        public void ConvertAnother(object value) {
            if (alreadySerialised.ContainsKey(value))
                writer.WriteAttribute(Attributes.references, alreadySerialised[value]);
            else {
                alreadySerialised.Add(value, writer.CurrentPath);
                ConvertObject(value);
            }
        }

        private void ConvertObject(object value) {
            Converter converter = ConverterLookup.GetConverter(value);
            if (converter != null)
                converter.ToXml(value, writer, this);
            else
                new Marshaller(writer, this).Marshal(value);
        }

        public void ConvertOriginal(object value) {
            StartNode(value);
            ConvertAnother(value);
            writer.EndNode();
        }

//        public void ConvertOriginal(object value, int index) {
//            StartNode(value, index);
//            ConvertAnother(value);
//            writer.EndNode();
//        }

        private void StartNode(object value) {
            Type type = value.GetType();
            writer.StartNode(Xmlifier.XmlifyAndRemoveGenerics(type));
            if (type.IsGenericType) AddGenericAttributes(type);
        }

        private void AddGenericAttributes(Type type) {
            Type[] genericArguments = type.GetGenericArguments();
            writer.WriteAttribute(Attributes.numberOfGenericArgs, genericArguments.Length);
            for (int i = 0; i < genericArguments.Length; i++) {
                Type genericArgument = genericArguments[i];
                writer.WriteAttribute(Attributes.GenericArg(i), Xmlifier.Xmlify(genericArgument));
            }
        }

//        private void StartNode(object value, int index) {
//            writer.StartCollectionNode(value.GetType().FullName.Replace("[]", "-array"), index);
//        }
    }
}