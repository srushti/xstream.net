using System;
using System.Collections;
using System.Collections.Generic;
using XStream.Converters;
using XStream.Converters.Collections;

namespace XStream {
    internal class ConverterLookup {
        private static readonly List<Converter> converters = new List<Converter>();
        private static readonly Converter nullConverter = new NullConverter();

        static ConverterLookup() {
            converters.Add(new SingleValueConverter<int>(int.Parse));
            converters.Add(new SingleValueConverter<DateTime>(DateTime.Parse));
            converters.Add(new SingleValueConverter<double>(double.Parse));
            converters.Add(new SingleValueConverter<long>(long.Parse));
            converters.Add(new SingleValueConverter<string>(delegate(string s) { return s; }));
            converters.Add(new ArrayConverter());
            converters.Add(new ListConverter());
        }

        internal static Converter GetConverter(Type type) {
            foreach (Converter converter in converters)
                if (converter.CanConvert(type)) return converter;
            return null;
        }

        public static Converter GetConverter(string typeName) {
            if (typeName.EndsWith("-array")) return GetConverter(typeof (Array));
            if (typeName.EndsWith("-list")) return GetConverter(typeof (ArrayList));
            return GetConverter(PrimitiveClassNamed(typeName));
        }

        private static Type PrimitiveClassNamed(String name) {
            return Type.GetType(name);
        }

        public static Converter GetConverter(object value) {
            if (value == null) return nullConverter;
            return GetConverter(value.GetType());
        }
    }
}