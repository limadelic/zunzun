namespace Zunzun.Specs.Fixtures {

    public class RefreshingTweets : Spec {
        
        protected override void SetUpSteps() {

            Given["Home is shown"] = () => {};
            And[@"the Refresh Cycle is ""Refresh Cycle"" seconds"] = () => {};
            When[@"Status is updated to ""Tweet Content"""] = () => {};
            And[@"""2"" seconds have passed"] = () => {};
            Then[@"Home should contain ""Tweet Content"""] = () => {};
        }
    }
}