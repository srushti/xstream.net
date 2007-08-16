using System;
using XStream.Converters;

namespace XStream {
    public class MarshallingContext {
        private readonly AlreadySerialisedDictionary alreadySerialised = new AlreadySerialisedDictionary();
        private readonly Writer writer;

        internal MarshallingContext(Writer writer) {
            this.writer = writer;
        }

        internal void ConvertAnother(object value) {
            Converter converter = ConverterLookup.GetConverter(value);
            if (converter != null) converter.ToXml(value, writer, this);
            else ConvertObject(value);
        }

        private void ConvertObject(object value) {
            if (alreadySerialised.ContainsKey(value))
                writer.WriteAttribute(Attributes.references, alreadySerialised[value]);
            else {
                alreadySerialised.Add(value, writer.CurrentPath);
                new Marshaller(writer, this).Marshal(value);
            }
        }

        public void ConvertOriginal(object value) {
            StartNode(value);
            ConvertAnother(value);
            writer.EndNode();
        }

        private void StartNode(object value) {
            Type type = value != null ? value.GetType() : typeof (object);
            writer.StartNode(Xmlifier.Xmlify(type));
            if (type.IsGenericType) AddGenericAttributes(type);
            else if (type.IsArray && type.GetElementType().IsGenericType) AddGenericAttributes(type);
        }

        private void AddGenericAttributes(Type type) {
            Type[] genericArguments = type.GetGenericArguments();
            writer.WriteAttribute(Attributes.numberOfGenericArgs, genericArguments.Length);
            for (int i = 0; i < genericArguments.Length; i++) {
                Type genericArgument = genericArguments[i];
                writer.WriteAttribute(Attributes.GenericArg(i), genericArgument.FullName);
            }
        }
    }
}