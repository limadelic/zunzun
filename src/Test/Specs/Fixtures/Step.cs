using System;
using System.Collections.Generic;

namespace Zunzun.Specs.Fixtures {

    public class Step {
    
        public Step(string Prototype) {
            Name = NameFrom(Prototype);
            Args = ArgsFrom(Prototype);
        }
    
        public Step(string Prototype, Action Action) : this(Prototype) {
            this.Action = Action;
        }

        public Step(string Prototype, Action<string> Action1) : this(Prototype) {
            this.Action1 = Action1;
        }

        string NameFrom(string Prototype) {
            var NameArgs = Prototype.Split(new[]{'"'});
            var name = "";
            
            for (var i = 0; i < NameArgs.Length; i += 2) 
                name += NameArgs[i];

            return name;
        }
        
        List<string> ArgsFrom(string Prototype) {
            var NameArgs = Prototype.Split(new[]{'"'});
            var args = new List<string>();
            
            for (var i = 1; i < NameArgs.Length; i += 2) 
                args.Add(NameArgs[i]);

            return args;
        }
        
        public string Name = string.Empty;
        public List<string> Args = new List<string>();

        public void Execute() {
            if (Args.Count == 0) Action();
            else Action1(Args[0]);
        }

        public Action Action;
        public Action<string> Action1;
    }
}