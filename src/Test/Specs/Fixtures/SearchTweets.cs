namespace Zunzun.Specs.Fixtures {

    public class SearchTweets : Spec{
    
        protected override void SetUpSteps() {
        
            When[@"I search for ""Text"""] = () => { };
            
            Then["I should find cool Tweets"] = () => { Fail(); };
        }
    }
}