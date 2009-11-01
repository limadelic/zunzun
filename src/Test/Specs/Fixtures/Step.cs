using System;
using System.Collections.Generic;

namespace Zunzun.Specs.Fixtures {

    public class Step {

        public string Name = string.Empty;
        public string Prototype = string.Empty;
        public Dictionary<string, string> Args = new Dictionary<string, string>();
        public Action Execute;
    }
}