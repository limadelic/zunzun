using System;
using fit;

namespace Zunzun.Specs.Helpers {

    public static class FitnesseExtensions {
        
        public static Parse ForEach(this Parse Parse, Action<Parse> Process) {
            for (var each = Parse; each != null; each = each.More)
                Process(each);
            return Parse;
        }
        
        public static bool Worked(this object obj, Action Action) {
            try {
                Action();
                return true;
            }  catch (Exception e) {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}