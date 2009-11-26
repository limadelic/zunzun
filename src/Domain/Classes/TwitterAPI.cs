using Dimebrain.TweetSharp.Fluent;

namespace Zunzun.Domain.Classes {

    public static class TwitterAPI {
        
        static IFluentTwitter Request { get { return FluentTwitter.CreateRequest(); }}
        
        static IFluentTwitter AuthenticatedRequest { get { return 
            Request.AuthenticateAs(Settings.UserName, Settings.Password)
        ;}}

        public static ITwitterUsers Users { get { return Request.Users();}}
        
        public static ITwitterUserFriends Friends { get { return AuthenticatedRequest.Users().GetFriends(); }}

        public static ITwitterFriendships Friendships { get { return AuthenticatedRequest.Friendships(); }}

        public static ITwitterStatuses Statuses { get { return 
            AuthenticatedRequest.Statuses()
        ;}}

        public static ITwitterHomeTimeline HomeStatuses { get { return 
            Statuses.OnHomeTimeline()
        ;}}

        public static ITwitterUserTimeline UserStatuses { get { return 
            Statuses.OnUserTimeline()
        ;}}
    }
}