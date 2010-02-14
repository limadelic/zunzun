using Zunzun.Domain;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs.Fixtures {

    public class SearchTweets {

        readonly TweetService TweetService = ObjectFactory.NewTweetService;
        
        public bool It_should_find_Tweets() { return Verify.That(() => 
            TweetService.TweetsContaining("lol").ShouldNotBeEmpty()
        );}
    }
}