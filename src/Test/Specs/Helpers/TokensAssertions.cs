using System.Windows.Documents;
using FluentSpec;

namespace Zunzun.Specs.Helpers {

    public static class TokensAssertions {
        
        public static void ShouldBeALiteralWith(this Inline Token, string Content) {
            Token.ShouldBeA<Run>();
            (Token as Run).Text.ShouldBe(Content);
        }

        public static void ShouldBeALinkTo(this Inline Token, string Url) {
            Token.ShouldBeA<Hyperlink>();
            (Token as Hyperlink).NavigateUri.AbsoluteUri.ShouldBe(Url);
        }
    }
}