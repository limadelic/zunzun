using System;
using Dimebrain.TweetSharp.Fluent;
using System.Collections.Generic;
using FluentSpec;
using Zunzun.Domain;
using Zunzun.Domain.Classes;
using ObjectFactory=Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs.Helpers {
    
    public static class Actors {

        public static string FiveRawTweets { get { return
            "[{\"in_reply_to_status_id\":null,\"favorited\":false,\"in_reply_to_user_id\":null,\"geo\":null,\"in_reply_to_screen_name\":null,\"source\":\"web\",\"created_at\":\"Wed Oct 14 03:47:20 +0000 2009\",\"user\":{\"geo_enabled\":false,\"profile_text_color\":\"000000\",\"followers_count\":1,\"description\":null,\"statuses_count\":1,\"profile_background_image_url\":\"http://s.twimg.com/a/1255464717/images/themes/theme1/bg.png\",\"friends_count\":5,\"profile_link_color\":\"0000ff\",\"profile_background_tile\":false,\"url\":null,\"following\":false,\"favourites_count\":0,\"profile_background_color\":\"9ae4e8\",\"profile_image_url\":\"http://s.twimg.com/a/1255464717/images/default_profile_5_normal.png\",\"verified\":false,\"created_at\":\"Sun Oct 11 18:51:55 +0000 2009\",\"profile_sidebar_fill_color\":\"e0ff92\",\"screen_name\":\"kinobot\",\"protected\":false,\"profile_sidebar_border_color\":\"87bc44\",\"location\":null,\"name\":\"kinobot\",\"notifications\":false,\"id\":81659009,\"time_zone\":null,\"utc_offset\":null},\"truncated\":false,\"id\":4853808689,\"text\":\"update myself\"},{\"in_reply_to_status_id\":null,\"favorited\":false,\"in_reply_to_user_id\":null,\"geo\":null,\"in_reply_to_screen_name\":null,\"source\":\"<a href=\\\"http://www.seesmic.com/\\\" rel=\\\"nofollow\\\">Seesmic</a>\",\"created_at\":\"Wed Oct 14 02:46:23 +0000 2009\",\"user\":{\"profile_text_color\":\"333333\",\"description\":\"natural born programmer\",\"statuses_count\":559,\"profile_background_image_url\":\"http://a3.twimg.com/profile_background_images/12641165/tech_background.gif\",\"verified\":false,\"profile_link_color\":\"0084B4\",\"profile_background_tile\":true,\"url\":\"http://msuarz.blogspot.com/\",\"following\":null,\"profile_background_color\":\"9AE4E8\",\"followers_count\":59,\"profile_image_url\":\"http://a3.twimg.com/profile_images/264581807/mini_normal.png\",\"notifications\":null,\"created_at\":\"Sun May 10 02:48:15 +0000 2009\",\"friends_count\":130,\"profile_sidebar_fill_color\":\"CCCCCC\",\"screen_name\":\"msuarz\",\"protected\":false,\"geo_enabled\":false,\"favourites_count\":1,\"profile_sidebar_border_color\":\"999999\",\"location\":\"miami\",\"name\":\"mike suarez\",\"id\":38981100,\"time_zone\":\"Eastern Time (US & Canada)\",\"utc_offset\":-18000},\"truncated\":false,\"id\":4852453987,\"text\":\"RT @HackerChick: wow! RT @jonbettinger RT @AdrianneMachina: Now *THIS* is how to do a \\\"site under construction!\\\" http://twurl.nl/67wzio\"},{\"favorited\":false,\"in_reply_to_user_id\":null,\"geo\":null,\"in_reply_to_screen_name\":null,\"source\":\"<a href=\\\"http://www.tweetdeck.com/\\\" rel=\\\"nofollow\\\">TweetDeck</a>\",\"created_at\":\"Tue Oct 13 11:42:45 +0000 2009\",\"truncated\":false,\"user\":{\"geo_enabled\":false,\"statuses_count\":555,\"profile_background_tile\":true,\"followers_count\":58,\"description\":\"natural born programmer\",\"profile_background_color\":\"9AE4E8\",\"friends_count\":130,\"profile_image_url\":\"http://a3.twimg.com/profile_images/264581807/mini_normal.png\",\"profile_sidebar_fill_color\":\"CCCCCC\",\"url\":\"http://msuarz.blogspot.com/\",\"following\":null,\"favourites_count\":1,\"screen_name\":\"msuarz\",\"verified\":false,\"profile_sidebar_border_color\":\"999999\",\"created_at\":\"Sun May 10 02:48:15 +0000 2009\",\"time_zone\":\"Eastern Time (US & Canada)\",\"protected\":false,\"profile_text_color\":\"333333\",\"location\":\"miami\",\"name\":\"mike suarez\",\"notifications\":null,\"profile_background_image_url\":\"http://a3.twimg.com/profile_background_images/12641165/tech_background.gif\",\"id\":38981100,\"utc_offset\":-18000,\"profile_link_color\":\"0084B4\"},\"in_reply_to_status_id\":null,\"id\":4832944979,\"text\":\"RT @markhneedham: today we saw a refactoring and instead of thinking about how good it'd be to do...we did it. Good times :-D\"},{\"favorited\":false,\"in_reply_to_user_id\":null,\"geo\":null,\"in_reply_to_screen_name\":null,\"source\":\"web\",\"created_at\":\"Tue Oct 13 07:05:46 +0000 2009\",\"truncated\":false,\"user\":{\"verified\":false,\"profile_background_tile\":true,\"description\":\"hand raiser\",\"friends_count\":5,\"profile_background_color\":\"1A1B1F\",\"notifications\":null,\"profile_image_url\":\"http://a3.twimg.com/profile_images/227924349/twitter_normal.jpg\",\"favourites_count\":2,\"profile_sidebar_fill_color\":\"252429\",\"url\":null,\"following\":null,\"screen_name\":\"RobertRdz\",\"profile_sidebar_border_color\":\"181A1E\",\"geo_enabled\":false,\"created_at\":\"Mon May 04 23:10:24 +0000 2009\",\"statuses_count\":51,\"time_zone\":\"Central Time (US & Canada)\",\"protected\":false,\"profile_text_color\":\"666666\",\"location\":\"Texas\",\"name\":\"Robert Rodriguez\",\"profile_background_image_url\":\"http://a3.twimg.com/profile_background_images/26339597/twitter.jpg\",\"id\":37788181,\"utc_offset\":-21600,\"profile_link_color\":\"2FC2EF\",\"followers_count\":10838},\"in_reply_to_status_id\":null,\"id\":4829814850,\"text\":\"killer first day of Predators.\"},{\"truncated\":false,\"in_reply_to_status_id\":null,\"favorited\":false,\"source\":\"<a href=\\\"http://www.tweetdeck.com/\\\" rel=\\\"nofollow\\\">TweetDeck</a>\",\"in_reply_to_user_id\":null,\"geo\":null,\"in_reply_to_screen_name\":null,\"created_at\":\"Mon Oct 12 22:30:26 +0000 2009\",\"user\":{\"geo_enabled\":false,\"followers_count\":59,\"description\":\"natural born programmer\",\"time_zone\":\"Eastern Time (US & Canada)\",\"friends_count\":130,\"profile_text_color\":\"333333\",\"statuses_count\":559,\"profile_background_image_url\":\"http://a3.twimg.com/profile_background_images/12641165/tech_background.gif\",\"url\":\"http://msuarz.blogspot.com/\",\"following\":true,\"favourites_count\":1,\"profile_link_color\":\"0084B4\",\"profile_image_url\":\"http://a3.twimg.com/profile_images/264581807/mini_normal.png\",\"verified\":false,\"profile_background_tile\":true,\"created_at\":\"Sun May 10 02:48:15 +0000 2009\",\"profile_background_color\":\"9AE4E8\",\"screen_name\":\"msuarz\",\"protected\":false,\"profile_sidebar_fill_color\":\"CCCCCC\",\"location\":\"miami\",\"name\":\"mike suarez\",\"notifications\":false,\"id\":38981100,\"utc_offset\":-18000,\"profile_sidebar_border_color\":\"999999\"},\"id\":4819273125,\"text\":\"RT @chadmyers: Heard another anecdote today of a guy hired as  'architect' but his bosses really meant \\\"Developer slave driver and whipman\\\"\"}]"
        ;}}
        public static ITwitterLeafNode FiveTweetsTestSpec { get { return
            SpecFrom(FiveRawTweets)
        ;}}

        public static Tweet UniqueTweet {
            get { return ObjectFactory.NewTweet(Guid.NewGuid().ToString()); }
        }

        public static Tweet TweetWithUser { get { return new 
            TweetClass { Author = Zunzun }
        ;}}
        
        public static List<Tweet> TwoTweets { get { 
            return new List<Tweet> { UniqueTweet, UniqueTweet };
        }}

        public static int OneMillisecond { get { return 1; } }
        public static int OneHundredMilliseconds { get { return 100; } }

        public const string KinobotUserName = "kinobot";
        public const string KinobotPassword = "kashmir";
        public const string KinobotEncryptedPassword = "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAUpGgy8CYCEu0OiEsuAN/RgAAAAACAAAAAAADZgAAwAAAABAAAACGv1FM7XN4Eh2f4G3HzaFEAAAAAASAAACgAAAAEAAAAARGXRIGFX2wHUJ4bkNxc9cQAAAAyCcTM/lESDvk3c1sRYLVTBQAAADBeZvK+elNVbzRQOAg1PkwlYSC0A==";

        public const string ZunzunUserName = "zunzunapp";
        public const string RawZunzun = "{\"following\":null,\"geo_enabled\":false,\"profile_background_tile\":false,\"description\":\"snappy & concise twitter app\",\"profile_background_color\":\"9ae4e8\",\"status\":{\"in_reply_to_user_id\":null,\"in_reply_to_status_id\":null,\"truncated\":false,\"favorited\":false,\"in_reply_to_screen_name\":null,\"source\":\"web\",\"created_at\":\"Sun Nov 08 16:44:29 +0000 2009\",\"id\":5535183525,\"text\":\"test\"},\"favourites_count\":0,\"profile_sidebar_fill_color\":\"e0ff92\",\"url\":\"http://zunzun.us\",\"verified\":false,\"notifications\":null,\"statuses_count\":1,\"time_zone\":\"Eastern Time (US & Canada)\",\"friends_count\":1,\"profile_sidebar_border_color\":\"87bc44\",\"created_at\":\"Fri Nov 06 01:38:13 +0000 2009\",\"profile_image_url\":\"http://a1.twimg.com/profile_images/516155390/logosq_normal.png\",\"protected\":false,\"profile_text_color\":\"000000\",\"location\":\"Miami, FL\",\"screen_name\":\"zunzunapp\",\"name\":\"zunzun\",\"profile_background_image_url\":\"http://s.twimg.com/a/1258070043/images/themes/theme1/bg.png\",\"followers_count\":2,\"id\":87839102,\"utc_offset\":-18000,\"profile_link_color\":\"0000ff\"}";

        public static ITwitterLeafNode ZunzunTestSpec { get { return 
            SpecFrom(RawZunzun)
        ;}}
        
        public static User Zunzun { get { return new UserClass {
            UserName = ZunzunUserName
        };}}

        public static Tweet TweetWithUserAndContent { get { 
            var tweet = TweetWithUser;
            tweet.Content = "good day today";
            return tweet;
        } }

        public static Tweet TweetWithUserAndId { get { 
            var tweet = TweetWithUser;
            tweet.Id = 42;
            return tweet;
        }}

        const string RawFollowingTwoUsers = @"{""profile_sidebar_fill_color"":""CCCCCC"",""description"":""natural born programmer"",""screen_name"":""msuarz"",""friends_count"":147,""status"":{""in_reply_to_user_id"":4999611,""in_reply_to_status_id"":null,""truncated"":false,""source"":""<a href=\""http://www.tweetdeck.com/\"" rel=\""nofollow\"">TweetDeck</a>"",""favorited"":false,""in_reply_to_screen_name"":""futureturnip"",""created_at"":""Tue Nov 24 00:19:01 +0000 2009"",""id"":5992629515,""text"":""@futureturnip u know fluentspec was the first mock framework to work with silverlight ... n' no one cared :) #Pyrrhic_victory""},""following"":true,""statuses_count"":660,""time_zone"":""Eastern Time (US & Canada)"",""profile_sidebar_border_color"":""999999"",""notifications"":false,""favourites_count"":1,""geo_enabled"":false,""profile_text_color"":""333333"",""url"":""http://msuarz.blogspot.com/"",""profile_background_image_url"":""http://a3.twimg.com/profile_background_images/12641165/tech_background.gif"",""verified"":false,""profile_link_color"":""0084B4"",""protected"":false,""profile_background_tile"":true,""created_at"":""Sun May 10 02:48:15 +0000 2009"",""location"":""miami"",""name"":""mike suarez"",""profile_background_color"":""9AE4E8"",""profile_image_url"":""http://a3.twimg.com/profile_images/264581807/mini_normal.png"",""id"":38981100,""utc_offset"":-18000,""followers_count"":74},{""profile_sidebar_fill_color"":""e0ff92"",""description"":null,""screen_name"":""jperkelens"",""friends_count"":11,""status"":{""in_reply_to_user_id"":14291248,""in_reply_to_status_id"":4112364670,""truncated"":false,""source"":""<a href=\""http://www.tweetdeck.com/\"" rel=\""nofollow\"">TweetDeck</a>"",""favorited"":false,""in_reply_to_screen_name"":""daric"",""created_at"":""Sun Sep 20 04:39:46 +0000 2009"",""id"":4117981485,""text"":""@daric I'm guessing ND did well this week...""},""following"":true,""statuses_count"":23,""time_zone"":null,""profile_sidebar_border_color"":""87bc44"",""notifications"":false,""favourites_count"":0,""geo_enabled"":false,""profile_text_color"":""000000"",""url"":null,""profile_background_image_url"":""http://s.twimg.com/a/1258674567/images/themes/theme1/bg.png"",""verified"":false,""profile_link_color"":""0000ff"",""protected"":false,""profile_background_tile"":false,""created_at"":""Fri Jul 31 16:30:51 +0000 2009"",""location"":null,""name"":""Jan Paul Erkelens"",""profile_background_color"":""9ae4e8"",""profile_image_url"":""http://a1.twimg.com/profile_images/393146608/avatar_normal.jpg"",""id"":61810086,""utc_offset"":null,""followers_count"":15}";

        public static ITwitterLeafNode FollowingTwoUsersTestSpec { get { return 
            SpecFrom(RawFollowingTwoUsers)
        ;}}

        public static List<User> TwoUsers { get { return new List<User> {
            Zunzun, Zunzun    
        };}}
        
        const string RawCredentials = "{\"profile_sidebar_fill_color\":\"e0ff92\",\"description\":null,\"screen_name\":\"kinobot\",\"friends_count\":28,\"status\":{\"in_reply_to_user_id\":null,\"in_reply_to_status_id\":null,\"truncated\":false,\"source\":\"web\",\"favorited\":false,\"in_reply_to_screen_name\":null,\"created_at\":\"Sun Nov 29 20:20:44 +0000 2009\",\"id\":6178720450,\"text\":\"0981550c-c371-4508-a8b5-86f3d1f548eb\"},\"following\":false,\"statuses_count\":202,\"time_zone\":null,\"profile_sidebar_border_color\":\"87bc44\",\"notifications\":false,\"favourites_count\":0,\"geo_enabled\":false,\"profile_text_color\":\"000000\",\"url\":null,\"profile_background_image_url\":\"http://s.twimg.com/a/1259091217/images/themes/theme1/bg.png\",\"verified\":false,\"profile_link_color\":\"0000ff\",\"protected\":false,\"profile_background_tile\":false,\"created_at\":\"Sun Oct 11 18:51:55 +0000 2009\",\"location\":null,\"name\":\"kinobot\",\"profile_background_color\":\"9ae4e8\",\"profile_image_url\":\"http://s.twimg.com/a/1259091217/images/default_profile_5_normal.png\",\"id\":81659009,\"utc_offset\":null,\"followers_count\":5}";
        
        public static ITwitterLeafNode CredentialsSpec { get { return
            SpecFrom(RawCredentials)
        ;}}
        
        const string RawError = "{\"request\":\"/account/verify_credentials.json\",\"error\":\"Could not authenticate you.\"}";

        public static ITwitterLeafNode ErrorSpec { get { return 
            SpecFrom(RawError)
        ;}}

        static ITwitterLeafNode SpecFrom(string Response) {
            var Spec = Create.TestObjectFor<ITwitterLeafNode>();
            Spec.Given().Request().WillReturn(Response);
            return Spec;
        }
    }
}