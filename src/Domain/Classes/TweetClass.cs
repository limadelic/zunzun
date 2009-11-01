namespace Zunzun.Domain.Classes {
    
    public class TweetClass : Tweet {
    
        public string Content { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Source { get; set; }
        public string Avatar { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(TweetClass Other) {
            if (ReferenceEquals(null, Other)) return false;
            if (ReferenceEquals(this, Other)) return true;
            return Equals(Other.Content, Content);
        }

        public override int GetHashCode() {
            return (Content != null ? Content.GetHashCode() : 0);
        }
    }
}