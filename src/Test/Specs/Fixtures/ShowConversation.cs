namespace Zunzun.Specs.Fixtures {

    public class ShowConversation : Spec {
    
        protected override void SetUpSteps() {

            Given("I say {0}", Something => Pending());

            When("I look at the Conversation", Pending);

            Then("it should say {0}", Something => Pending());
        }
    }
}