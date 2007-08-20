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
            converters.Add(new SingleValueConverter<short>(short.Parse));
            converters.Add(new SingleValueConverter<long>(long.Parse));
            converters.Add(new SingleValueConverter<double>(double.Parse));
            converters.Add(new SingleValueConverter<UInt16>(UInt16.Parse));
            converters.Add(new SingleValueConverter<UInt32>(UInt32.Parse));
            converters.Add(new SingleValueConverter<UInt64>(UInt64.Parse));
            converters.Add(new SingleValueConverter<DateTime>(DateTime.Parse));
            converters.Add(new SingleValueConverter<Single>(Single.Parse));
            converters.Add(new SingleValueConverter<decimal>(decimal.Parse));
            converters.Add(new SingleValueConverter<bool>(bool.Parse));
            converters.Add(new SingleValueConverter<byte>(byte.Parse));
            converters.Add(new SingleValueConverter<Guid>(delegate(string s) { return new Guid(s); }));
            converters.Add(new TypeConverter());
            converters.Add(new SingleValueConverter<string>(delegate(string s) { return s; }));
            converters.Add(new SingleValueConverter<char>(char.Parse));
            converters.Add(new EnumConverter());
            converters.Add(new HashtableConverter());
            converters.Add(new ArrayConverter());
            converters.Add(new ListConverter());
        }

        internal static Converter GetConverter(Type type) {
            if (type == null) return null;
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