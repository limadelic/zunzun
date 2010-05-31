using System;
using System.Collections.Generic;
using System.Linq;
using fit;

namespace Zunzun.Specs.Helpers {

    public static class FitnesseExtensions {
        
        public static Parse ForEach(this Parse Parse, Action<Parse> Process) {
            for (var each = Parse; each != null; each = each.More)
                Process(each);
            return Parse;
        }

        public static List<object> ToNamedObjectList(this List<string> Names) {
            return Names.Select(Name => new { Name } as object).ToList();
        }
    }
}