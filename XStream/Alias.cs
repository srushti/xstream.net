using System;
using System.Collections.Generic;

namespace xstream {
    public interface Alias {
        bool TryGetType(string alias, out Type type);
        bool TryGetAlias(Type type, out string alias);
    }

    public class Aliases : List<Alias> {}
}