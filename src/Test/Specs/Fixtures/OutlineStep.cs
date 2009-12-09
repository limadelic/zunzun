using System.Collections.Generic;

namespace Zunzun.Specs.Fixtures {

    public class OutlineStep {
    
        public List<string> OutlineArgs { get; private set; }
        public List<int> OutlineCols { get; private set; }
        
        readonly Step Core;
        
        public OutlineStep(Step Core) {
            this.Core = Core;
            OutlineArgs = new List<string>(Core.Args);
        }
        
        public void MapArgsTo(List<string> ScenarioCols) { 
            OutlineCols = new List<int>();
            
            OutlineArgs.ForEach(Arg =>
                OutlineCols.Add(ScenarioCols.IndexOf(Arg)));
        }

        public void Execute() { Core.Execute(); }
        
        public List<string> Args { get { return Core.Args; } }

        public override string ToString() { return Core.ToString(); }
    }
}