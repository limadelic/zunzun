using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zunzun.Specs.Fixtures
{
    public class ReplyToTweet : Spec 
    {
        
        protected override void SetUpSteps()
        {
            Given[@"a tweet by user ""zunzun"""] = () =>
            {
                Assert.Fail("Test Not Implemented");
            };

            When["I reply to the Tweet"] = () =>
            {
                Assert.Fail("Test Not Implemented");
            };

            Then[@"Update Text contains ""@zunzun"""] = () =>
            {
                Assert.Fail("Test Not Implemented");
            };
        }
    }
}