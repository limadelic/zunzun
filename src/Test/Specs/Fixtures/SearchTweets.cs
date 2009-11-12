namespace Zunzun.Specs.Fixtures {

    public class SearchTweets : Spec {
    
        protected override void SetUpSteps() {
        
            When(@"I search for ""0""", Text => {});
            
            Then("I should find cool Tweets", () => Fail());
        }
    }
}