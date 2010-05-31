using System;
using fit;

namespace Zunzun.Specs.Helpers {

    public static class FitnesseExtensions {
        
        public static Parse ForEach(this Parse Parse, Action<Parse> Process) {
            for (var each = Parse; each != null; each = each.More)
                Process(each);
            return Parse;
        }
    }
}