namespace Zunzun.Domain {

    public interface Tweet {
    
        long Id { get; set; }
        long ReplyTo { get; set; }
        
        string Content { get; set; }
        string Author { get; set; }
        string Date { get; set; }
        string Source { get; set; }
        string Picture { get; set; }
        string ScreenName { get; set; }
    }
}