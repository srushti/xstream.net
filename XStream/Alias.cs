using System;
using System.Collections.Generic;

namespace xstream {
    public interface Alias {
        bool TryGetType(string alias, out Type type);
        bool TryGetAlias(Type type, out string aliasToReturn);
    }

    internal class Aliases : List<Alias> {}

    public abstract class StandardAlias : Alias {
        public bool TryGetType(string alias, out Type typeToReturn) {
            typeToReturn = Type;
            return Type.Name.Equals(alias);
        }

        protected abstract Type Type { get; }

        public bool TryGetAlias(Type type, out string aliasToReturn) {
            aliasToReturn = Alias;
            return Type.Equals(type);
        }

        protected abstract string Alias { get; }
    }
}