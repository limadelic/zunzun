using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Zunzun.App.Model;
using FluentSpec;

namespace Zunzun.Specs.Fixtures {

    public class ProvideLinksInTweetContent : Spec  {

        readonly TextFormatter TextFormatter = App.ObjectFactory.NewTextFormatter;

        string Text;
        IEnumerable<Inline> Tokens;
    
        protected override void SetUpSteps() { 
            
            Given[@"the Tweet ""Content"""] = () => {
                Text = Expected["Content"];
            };

            When["it is formatted for displaying"] = () => {
                Tokens = TextFormatter.TokensFrom(Text);
            };

            Then["it should not contain any links"] = () => {
                Tokens.Any(Inline => Inline is Hyperlink).ShouldBeFalse();
            };

            Then[@"it should contain a link to ""Url"""] = () => {
                Tokens.Any(Inline => 
                    Inline is Hyperlink 
                    && (Inline as Hyperlink).NavigateUri.AbsoluteUri.Contains(Expected["Url"])
                ).ShouldBeTrue();
            };
        }
    }
}