using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Model.Classes;
using Zunzun.Domain;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    [TestClass]
    public class when_formatting_Tweets_for_displaying : BehaviorOf<TextFormatterClass> {
    
        [TestMethod]
        public void words_should_become_a_single_literal() {

            var Tokens = The.TokensFrom("hello world!!");

            Tokens.Count.ShouldBe(1);
            Tokens[0].ShouldBeALiteralWith("hello world!!");
        }
        
        [TestMethod]
        public void the_extra_spaces_should_be_respected() {

            var Tokens = The.TokensFrom("  hello  world!!  ");

            Tokens.Count.ShouldBe(1);
            Tokens[0].ShouldBeALiteralWith("  hello  world!!  ");
        }
        
        [TestMethod]
        public void an_url_should_become_a_link() {

            var Tokens = The.TokensFrom("http://www.zunzun.com");

            Tokens[0].ShouldBeALinkTo("http://www.zunzun.com/");
        }
        
        [TestMethod]
        public void an_url_should_become_a_link_only_if_it_is_http_https_or_ftp() {

            The.TokensFrom("http://www.zunzun.com")[0]
                .ShouldBeALinkTo("http://www.zunzun.com/");
            The.TokensFrom("https://www.zunzun.com")[0]
                .ShouldBeALinkTo("https://www.zunzun.com/");
            The.TokensFrom("ftp://www.zunzun.com")[0]
                .ShouldBeALinkTo("ftp://www.zunzun.com/");
                
            The.TokensFrom("file://www.zunzun.com")[0]
                .ShouldBeALiteralWith("file://www.zunzun.com");
        }
        
        [TestMethod]
        public void a_Mention_should_become_a_link_to_User_Home() {

            var Tokens = The.TokensFrom("@zunzun");

            Tokens[0].ShouldBeALiteralWith("@");
            Tokens[1].ShouldBeALinkTo(Settings.TwitterUrl + "zunzun");
        }

        [TestMethod]
        public void a_Mention_with_a_suffix_should_not_include_it_in_the_link() {

            var Tokens = The.TokensFrom("@zunzun:");

            Tokens[1].ShouldBeALinkTo(Settings.TwitterUrl + "zunzun");
            Tokens[2].ShouldBeALiteralWith(":");
        }
        
        [TestMethod]
        public void an_url_embedded_in_text_should_become_a_link_and_literals() {
            
            var Tokens = The.TokensFrom("check this out http://www.zunzun.com ... very nice");

            Tokens[0].ShouldBeALiteralWith("check this out ");
            Tokens[1].ShouldBeALinkTo("http://www.zunzun.com/");
            Tokens[2].ShouldBeALiteralWith(" ... very nice");
        }
        
        [TestMethod]
        public void should_find_enclosed_url_and_Mentions() {
        
            The.TokensFrom("[http://www.zunzun.com]")[1]
                .ShouldBeALinkTo("http://www.zunzun.com/");

            The.TokensFrom("(@zunzun)")[1]
                .ShouldBeALinkTo(Settings.TwitterUrl + "zunzun");

            The.TokensFrom("{http://www.zunzun.com}")[1]
                .ShouldBeALinkTo("http://www.zunzun.com/");
        }
    }
}