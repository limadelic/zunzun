using System;
using System.Collections.Generic;
using fitnesse.fixtures;
using Zunzun.Specs.Helpers;

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

        public object Then(string Step) { return Do(Step); }
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
            
                if (OnScenarioOutline) 
                    OutlineSteps.Add(new OutlineStep(CurrentStep));
                else CurrentStep.Execute();
                
                return true;
                
            } catch (Exception e) {
                Error.Add(GetType().Name, CurrentStep.Name, e.Message);
                return false;
            }
        }

        bool OnScenarioOutline;
        List<OutlineStep> OutlineSteps;
        
        public void ScenarioOutline(string Name) {
            OnScenarioOutline = true;
            OutlineSteps = new List<OutlineStep>();
        }
        public TableFixture Scenarios(string Name) {
            OnScenarioOutline = false;
            return new Scenarios(OutlineSteps);
        }

        protected void Pending() { Fail("Pending step implementation"); }
        
        protected void Fail(string Message) { throw new Exception(Message); }
        
        protected void Debug() { System.Diagnostics.Debugger.Launch(); }
    }
}