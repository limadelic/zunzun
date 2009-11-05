using System;
using System.Collections.Generic;
using Zunzun.Domain.Helpers;
using System.Linq;

namespace Zunzun.Specs.Fixtures {

    public abstract class Spec {
    
        readonly Dictionary<string, Action> DefinedSteps = new Dictionary<string, Action>();
        readonly Dictionary<string, Step> Steps = new Dictionary<string, Step>();
        Step CurrentStep { get; set; }
        
        protected Dictionary<string, Action> Given { get { return DefinedSteps; } }
        protected Dictionary<string, Action> When { get { return DefinedSteps; } }
        protected Dictionary<string, Action> Then { get { return DefinedSteps; } }
        protected Dictionary<string, Action> It { get { return DefinedSteps; } }
        protected Dictionary<string, Action> And { get { return DefinedSteps; } }

        protected Dictionary<string, string> Var { get { return CurrentStep.Args; } }
        
        protected Spec() {
            SetUpSteps();
            BuildSteps();
        }

        void BuildSteps() { DefinedSteps.ForEach(BuildStep); }
        
        void BuildStep(KeyValuePair<string, Action> Prototype) {
            
            var Step = StepFrom(Prototype.Key);
            
            Steps[Step.Name] = new Step {
                Name = Step.Name,
                Args = Step.Args,
                Execute = Prototype.Value,
            };
        }

        Step StepFrom(string Prototype) {
            var Step = new Step();
            var NameArgs = Prototype.Split(new[]{'"'});
            
            for (var i = 0; i < NameArgs.Length; i++) 
                if (i % 2 == 0) Step.Name += NameArgs[i];
                else Step.Args[NameArgs[i]] = string.Empty;

            return Step;
        }

        void SetupCurrentStepFrom(string DesiredStepPrototype) {
            Action Fail = () => { throw new Exception("Missing implementation for '" + DesiredStepPrototype + "'"); };

            var DesiredStep = StepFrom(DesiredStepPrototype);
            
            if (!Steps.ContainsKey(DesiredStep.Name)) Fail();

            var CorrespondingStep = Steps[DesiredStep.Name];
            
            if (DesiredStep.Args.Count != CorrespondingStep.Args.Count) Fail();

            CurrentStep = CorrespondingStep;

            FillCurrentStepArgsFrom(DesiredStep);
        }

        void FillCurrentStepArgsFrom(Step DesiredStep) {

            var i = 0;
            foreach (var ExpectedArg in DesiredStep.Args) 
                CurrentStep.Args[CurrentStep.Args.Keys.ElementAt(i++)] = ExpectedArg.Key;
        }

        protected abstract void SetUpSteps();

        public void given(string Step) { Do(Step); }

        public void when(string Step) { Do(Step); }

        public bool and(string Step) { return Test(Step); }

        public bool it(string Step) { return Test(Step); }

        public bool then(string Step) { return Test(Step); }

        void Do(string DesiredStep) {
            SetupCurrentStepFrom(DesiredStep);
            CurrentStep.Execute();
        }

        bool Test(string DesiredStep) {
            SetupCurrentStepFrom(DesiredStep);
            
            try {

                CurrentStep.Execute();
                return true;
                
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
        protected void Fail() { Fail(string.Empty); }
        
        protected void Fail(string Message) { throw new Exception(Message); }
    }
}