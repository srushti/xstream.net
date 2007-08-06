using System;
using System.Collections;
using System.Collections.Generic;
using XStream.Converters;
using XStream.Converters.Collections;

namespace XStream {
    internal class ConverterLookup {
        private static readonly Dictionary<Type, Converter> converters = new Dictionary<Type, Converter>();
        private static readonly Converter nullConverter = new NullConverter();

        static ConverterLookup() {
            converters.Add(typeof (int), new IntConverter());
            converters.Add(typeof (string), new StringConverter());
            converters.Add(typeof (Array), new ArrayConverter());
            converters.Add(typeof (IList), new ListConverter());
        }

        internal static Converter GetConverter(Type type) {
            foreach (KeyValuePair<Type, Converter> pair in converters)
                if (pair.Value.CanConvert(type)) return pair.Value;
            return null;
        }

        public static Converter GetConverter(string typeName) {
            if (typeName.EndsWith("-array")) return converters[typeof (Array)];
            if (typeName.EndsWith("list")) return converters[typeof (IList)];
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