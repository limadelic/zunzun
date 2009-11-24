using System;

namespace Zunzun.Domain.Classes {
    
    public class TweetClass : Tweet {
    
        public long Id { get; set; }
        public long ReplyTo { get; set; }

        public string Content { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Source { get; set; }
        public string Picture { get; set; }
        public string ScreenName { get; set; }

        public override bool Equals(object obj) {
            if (!(obj is TweetClass)) return false;
            var other = obj as TweetClass;
            
            return ReferenceEquals(this, other) || Equals(other.Content, Content);
        }

        public override int GetHashCode() {
            return (Content != null ? Content.GetHashCode() : 0);
        }
    }
}