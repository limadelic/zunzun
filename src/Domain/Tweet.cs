namespace Zunzun.Domain {

    public interface Tweet {
    
        long Id { get; set; }
        long ReplyTo { get; set; }
        
        string Content { get; set; }
        User Author { get; set; }
        string Date { get; set; }
        string Source { get; set; }
        string Picture { get; set; }
    }
}