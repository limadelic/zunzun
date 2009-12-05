using System;
using System.Collections.Generic;
using fitnesse.fixtures;

namespace Zunzun.Specs.Fixtures {

    public abstract class Spec {
    
        readonly Dictionary<string, Step> Steps = new Dictionary<string, Step>();
        Step CurrentStep { get; set; }
        
        protected Spec() { SetUpSteps(); }

        protected abstract void SetUpSteps();
        
        void SetupCurrentStepFrom(string DesiredStepPrototype) {
            Action Fail = () => { throw new Exception("Missing implementation for '" + DesiredStepPrototype + "'"); };

            var DesiredStep = new Step(DesiredStepPrototype);
            
            if (!Steps.ContainsKey(DesiredStep.Name)) Fail();

            var CorrespondingStep = Steps[DesiredStep.Name];
            
            if (DesiredStep.Args.Count != CorrespondingStep.Args.Count) Fail();

            CurrentStep = CorrespondingStep;
            CurrentStep.Args = DesiredStep.Args;
        }

        void SetupStep(Step Step) { Steps[Step.Name] = Step; }

        public bool Given(string Step) { return Do(Step); }
        protected void Given(string Step, Action Action) {
            SetupStep(new Step(Step, Action));
        }
        protected void Given(string Step, Action<string> Action) {
            SetupStep(new Step(Step, Action));
        }
        protected void Given(string step, Action<string, string> action)
        {
            SetupStep(new Step(step, action));
        }

        public bool When(string Step) { return Do(Step); }
        protected void When(string Step, Action Action) {
            SetupStep(new Step(Step, Action));
        }
        protected void When(string Step, Action<string> Action) {
            SetupStep(new Step(Step, Action));
        }
        protected void When(string Step, Action<string, string> Action) {
            SetupStep(new Step(Step, Action));
        }

        public bool And(string Step) { return Do(Step); }
        protected void And(string Step, Action Action) {
            SetupStep(new Step(Step, Action));
        }
        protected void And(string Step, Action<string> Action) {
            SetupStep(new Step(Step, Action));
        }

        public bool Then(string Step) { return Do(Step); }
        protected void Then(string Step, Action Action) {
            SetupStep(new Step(Step, Action));
        }
        protected void Then(string Step, Action<string> Action) {
            SetupStep(new Step(Step, Action));
        }
        protected void Then(string Step, Action<string, string> Action) {
            SetupStep(new Step(Step, Action));
        }

        bool Do(string DesiredStep) {
        
            SetupCurrentStepFrom(DesiredStep);
            
            try {

                CurrentStep.Execute();
                return true;
                
            } catch (Exception e) {
                var Msg = "Failed on step [" + CurrentStep + "] due to [" + e.Message + "]";
                Console.WriteLine(Msg);
                return false;
            }
        }
        
        public void ScenarioOutline(string Name) { }
        public TableFixture Scenarios(string Name) { return new Scenarios(); }

        protected void Pending() { Fail("Pending step implementation"); }
        
        protected void Fail(string Message) { throw new Exception(Message); }
        
        protected void Debug() { System.Diagnostics.Debugger.Launch(); }
    }
}