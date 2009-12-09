using System;
using System.Collections.Generic;

namespace Zunzun.Specs.Fixtures {

    public class Step {
    
        public string Name = string.Empty;
        public List<string> Args = new List<string>();

        public Step(string Prototype) {

            var NameArgs = Prototype.Split(new[] {'"', '{', '}'});
            
            for (var i = 0; i < NameArgs.Length; i++)
                if ( i % 2 == 0) Name += NameArgs[i];
                else Args.Add(NameArgs[i]);
        }

        public Step(string Prototype, Action Action) : this(Prototype) { this.Action = Action; }

        public Step(string Prototype, Action<string> Action) : this(Prototype) { Action1 = Action; }

        public Step(string prototype, Action<string, string> Action) : this(prototype) { Action2 = Action; }
        
        readonly Action Action;
        readonly Action<string> Action1;
        readonly Action<string, string> Action2;

        public void Execute() { switch (Args.Count) {
            case 2: Action2(Args[0], Args[1]); return;
            case 1: Action1(Args[0]); return;
            default: Action(); return;
        }}

        public override string ToString() { return 
            String.Format(Name, Args)
        ;}
    }
}