using System;
using System.Collections;
using System.Collections.Generic;
using xstream.Converters;
using xstream.Converters.Collections;

namespace xstream {
    internal class ConverterLookup {
        private static readonly List<Converter> standardConverters = new List<Converter>();
        private readonly List<Converter> converters = new List<Converter>();
        private static readonly Converter nullConverter = new NullConverter();

        static ConverterLookup() {
            standardConverters.Add(new SingleValueConverter<int>(int.Parse));
            standardConverters.Add(new SingleValueConverter<short>(short.Parse));
            standardConverters.Add(new SingleValueConverter<long>(long.Parse));
            standardConverters.Add(new SingleValueConverter<double>(double.Parse));
            standardConverters.Add(new SingleValueConverter<UInt16>(UInt16.Parse));
            standardConverters.Add(new SingleValueConverter<UInt32>(UInt32.Parse));
            standardConverters.Add(new SingleValueConverter<UInt64>(UInt64.Parse));
            standardConverters.Add(new SingleValueConverter<DateTime>(DateTime.Parse));
            standardConverters.Add(new SingleValueConverter<Single>(Single.Parse));
            standardConverters.Add(new SingleValueConverter<decimal>(decimal.Parse));
            standardConverters.Add(new SingleValueConverter<bool>(bool.Parse));
            standardConverters.Add(new SingleValueConverter<byte>(byte.Parse));
            standardConverters.Add(new SingleValueConverter<Guid>(delegate(string s) { return new Guid(s); }));
            standardConverters.Add(new TypeConverter());
            standardConverters.Add(new SingleValueConverter<string>(delegate(string s) { return s; }));
            standardConverters.Add(new SingleValueConverter<char>(char.Parse));
            standardConverters.Add(new EnumConverter());
            standardConverters.Add(new HashtableConverter());
            standardConverters.Add(new ArrayConverter());
            standardConverters.Add(new ListConverter());
            standardConverters.Add(new DictionaryConverter());
        }

        public ConverterLookup() {
            converters.AddRange(standardConverters);
        }

        internal Converter GetConverter(Type type) {
            if (type == null) return null;
            foreach (Converter converter in converters)
                if (converter.CanConvert(type)) return converter;
            return null;
        }

        public Converter GetConverter(string typeName) {
            if (typeName.EndsWith("-array")) return GetConverter(typeof (Array));
            if (typeName.EndsWith("-list")) return GetConverter(typeof (ArrayList));
            return GetConverter(PrimitiveClassNamed(typeName));
        }

        private static Type PrimitiveClassNamed(String name) {
            return Type.GetType(name);
        }

        public Converter GetConverter(object value) {
            if (value == null) return nullConverter;
            return GetConverter(value.GetType());
        }

        public void AddConverter(Converter converter) {
            converters.Add(converter);
        }
    }
}