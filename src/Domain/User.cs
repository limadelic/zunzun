namespace Zunzun.Domain {

    public interface User {
        
        string Name { get; set; }
        string UserName { get; set; }
        string Bio { get; set; }
        string JoinedOn { get; set; }
        
        int Following { get; set; }
        int Followers { get; set; }
        int UpdatesCount { get; set; }
        
        string Website { get; set; }
        string TwitterHome { get; set; }
        
        string Location { get; set; }
        string TimeZone { get; set; }
    }
}