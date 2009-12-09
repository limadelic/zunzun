using System;
using System.Collections.Generic;
using fit;
using fitnesse.fixtures;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class Scenarios : TableFixture {
    
        public List<OutlineStep> OutlineSteps { get; set; }
        readonly List<string> ScenarioCols = new List<string>();

        public Scenarios(List<OutlineStep> OutlineSteps) {
            this.OutlineSteps = OutlineSteps;
        }

        protected override void DoStaticTable(int Rows) {
            SetUpScenarioCols();
            RunScenarios(Rows);
        }

        void RunScenarios(int Rows) {
            for (var i = 1; i < Rows; i++)
                RunScenario(i);
        }

        int Row;
        void RunScenario(int Row) { 
            this.Row = Row;
            
            OutlineSteps.ForEach(RunStep);
        }

        void RunStep(OutlineStep OutlineStep) {
            Specify(OutlineStep);
            
            try {
            
                OutlineStep.Execute();
                Right(OutlineStep);
                
            } catch(Exception Exception) { Wrong(OutlineStep, Exception); }
        }

        void Right(OutlineStep Step) {
            Step.OutlineCols.ForEach(Col => Right(Row, Col));
        }

        void Wrong(OutlineStep Step, Exception Exception) {
            var Error = "Failed on step [" + Step + "] due to [" + Exception.Message + "]";

            Step.OutlineCols.ForEach(Col => Wrong(Row, Col, Error));
        }

        void Specify(OutlineStep OutlineStep) {
            for (var i = 0; i < OutlineStep.Args.Count; i++)
                OutlineStep.Args[i] = GetString(
                    Row, OutlineStep.OutlineCols[i]
                );
        }

        Parse Header { get { return GetCell(0, 0); } }

        void SetUpScenarioCols() {
        
            Header.ForEach(Column => 
                ScenarioCols.Add(Column.Text));
                
            OutlineSteps.ForEach(Step => 
                Step.MapArgsTo(ScenarioCols));
        }
    }
}