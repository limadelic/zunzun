using System;
using System.Collections.Generic;

namespace Zunzun.Specs.Fixtures {

    public class Step {
    
        public Step(string Prototype) {

            var NameArgs = Prototype.Split(new[] {'"', '{', '}'});
            
            for (var i = 0; i < NameArgs.Length; i++)
                if ( i % 2 == 0) Name += NameArgs[i];
                else Args.Add(NameArgs[i]);
        }

        public Step(string Prototype, Action Action) : this(Prototype) { this.Action = Action; }

        public Step(string Prototype, Action<string> Action) : this(Prototype) { Action1 = Action; }
        
        public string Name = string.Empty;
        public List<string> Args = new List<string>();

        readonly Action Action;
        readonly Action<string> Action1;

        public void Execute() {
            if (Args.Count == 0) Action();
            else Action1(Args[0]);
        }
    }
}