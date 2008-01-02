using System.Collections.Generic;
using System.Reflection;
using System.Text;
using xstream.Converters;

namespace xstream {
    public class XStream {
        private readonly ConverterLookup converterLookup = new ConverterLookup();
        private readonly Aliases aliases = new Aliases();
        private readonly List<Assembly> assemblies = new List<Assembly>();

        public string ToXml(object value) {
            StringBuilder stringBuilder = new StringBuilder();
            XWriter writer = new XWriter(stringBuilder);
            MarshallingContext context = new MarshallingContext(writer, converterLookup, aliases);
            context.ConvertOriginal(value);
            return stringBuilder.ToString();
        }

        public void AddConverter(Converter converter) {
            converterLookup.AddConverter(converter);
        }

        public object FromXml(string s) {
            XReader reader = new XReader(s);
            UnmarshallingContext context = new UnmarshallingContext(reader, converterLookup, aliases, assemblies);
            return context.ConvertOriginal();
        }

        public void AddAlias(Alias alias) {
            aliases.Add(alias);
        }

        public void Load(params Assembly[] externalAssemblies) {
            if (externalAssemblies == null) return;
            assemblies.AddRange(externalAssemblies);
        }

        public void Unload(params Assembly[] externalAssemblies) {
            if (externalAssemblies == null) return;
            foreach (Assembly externalAssembly in externalAssemblies) assemblies.Remove(externalAssembly);
        }
    }
}