using System;

namespace Zunzun.Specs.Helpers {

    public static class Verify {
        
        public static bool That(Action Action) {
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