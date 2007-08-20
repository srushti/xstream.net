using System;
using System.Collections.Generic;
using XStream.Converters;
using XStream.Utilities;

namespace XStream {
    public class UnmarshallingContext {
        private readonly Dictionary<string, object> alreadyDeserialised = new Dictionary<string, object>();
        private readonly Reader reader;

        internal UnmarshallingContext(Reader reader) {
            this.reader = reader;
        }

        public object ConvertAnother() {
            string nullAttribute = reader.GetAttribute(Attributes.Null);
            if (nullAttribute != null && nullAttribute == "true") return null;
            object result = Find();
            if (result != null) return result;
            Converter converter = ConverterLookup.GetConverter(reader.GetNodeName());
            if (converter == null) return ConvertOriginal();
            return converter.FromXml(reader, this);
        }

        public object ConvertOriginal() {
            string typeName = reader.GetAttribute(Attributes.classType);
//            string genericArgsAttribute = reader.GetAttribute(Attributes.numberOfGenericArgs);
            Type type;
//            if (string.IsNullOrEmpty(genericArgsAttribute))
            type = Type.GetType(typeName);
//            else
//                type = GetGenericType(typeName, int.Parse(genericArgsAttribute));
            if (type == null) throw new ConversionException("Couldn't deserialise from " + typeName);
            Converter converter = ConverterLookup.GetConverter(type);
            if (converter != null) return converter.FromXml(reader, this);
            return new Unmarshaller(reader, this).Unmarshal(type);
        }

        private Type GetGenericType(string typeName, int noOfGenericArgs) {
            Type genericType = Type.GetType(S.RemoveFrom(typeName, "[]") + "`" + noOfGenericArgs);
            Type[] genericArgs = new Type[noOfGenericArgs];
            for (int i = 0; i < noOfGenericArgs; i++) genericArgs[i] = Type.GetType(Xmlifier.UnXmlify(reader.GetAttribute(Attributes.GenericArg(i))));
            if (typeName.Contains("[]")) return genericType.MakeArrayType();
            return genericType.MakeGenericType(genericArgs);
        }

        public void StackObject(object value) {
            alreadyDeserialised.Add(reader.CurrentPath, value);
        }

        public object Find() {
            string referencesAttribute = reader.GetAttribute(Attributes.references);
            if (!string.IsNullOrEmpty(referencesAttribute)) return alreadyDeserialised[referencesAttribute];
            return null;
        }
    }
}