namespace Zunzun.Domain.Classes {

    public class UserClass : User {
    
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
        public string Bio { get; set; }
        public string JoinedOn { get; set; }
        
        public int Following { get; set; }
        public int Followers { get; set; }
        public int UpdatesCount { get; set; }
        
        public string Website { get; set; }
        public string TwitterHome { get { return Settings.TwitterUrl + UserName; }}
        
        public string Location { get; set; }
        public string TimeZone { get; set; }
    }
}