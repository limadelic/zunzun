using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using Zunzun.Domain.Helpers;

namespace Zunzun.App.Model.Classes {

    public class TextFormatterClass : TextFormatter {

        List<Inline> Tokens;
        string Word;
        string Literal;
    
        public List<Inline> TokensFrom(string Text) {
            Tokens = new List<Inline>();

            var Words = Text.Split(new[] {' '});

            Words.ForEach(Tokenize);
            AddRemainingLiteral();
            
            return Tokens;
        }

        void Tokenize(string Word) { this.Word = Word;
        
            if (IsLink) TokenizeLink();
            else if (IsMention) TokenizeMention();
            else TokenizeLiteral();
        }

        protected bool IsMention { get { return 
            Word.StartsWith(Domain.Settings.MentionPreffix)
        ;}}

        bool IsLink { get { return
            Uri.IsWellFormedUriString(Word, UriKind.Absolute)
            && Domain.Settings.AcceptedProtocols.Contains(UriScheme)
        ;}}

        string UriScheme { get { return new Uri(Word).Scheme; } }

        void TokenizeLiteral() { Literal += Word + " "; }

        void TokenizeLink() {
            AddLiteralToken();
            AddLinkToken();
            SeparateFromNextLiteral();
        }

        void TokenizeMention() {
            AddMentionPrefixToLiteral();
            AddLiteralToken();
            AddSuffixToNextLiteral();
            AddMentionToken();
        }

        void AddMentionPrefixToLiteral() {
            Literal += "@";
            Word.Remove(0, 1);
        }

        void AddLiteralToken() {
            if (String.IsNullOrEmpty(Literal)) return;
            
            Tokens.Add(TokenFactory.NewLiteral(Literal));
        }

        void AddLinkToken() {
            Tokens.Add(TokenFactory.NewLink(Word));
        }

        void AddMentionToken() {
            Tokens.Add(TokenFactory.NewLinkToUserHome(Word));
        }

        void AddSuffixToNextLiteral() {
            var Mention = Regex.Match(Word, @"(\w+)(?<Suffix>.*)");

            Word = Mention.Groups[1].Value;
            Literal = Mention.Groups["Suffix"].Value + " ";
        }

        void SeparateFromNextLiteral() {
            Literal = " ";
        }

        void AddRemainingLiteral() {
            if (String.IsNullOrEmpty(Literal)) return;
            
            Literal = Literal.Remove(Literal.Length - 1);
            AddLiteralToken();
        }
    }
}