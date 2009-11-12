namespace Zunzun.Domain {

    public interface Tweet {
    
        string Content { get; set; }
        string Author { get; set; }
        string Date { get; set; }
        string Source { get; set; }
        string Avatar { get; set; }
        string ScreenName { get; set; }
    }
}