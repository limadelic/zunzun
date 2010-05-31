using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Converters;

namespace Zunzun.Specs {

    [TestClass]
    public class when_showing_a_Tweet : BehaviorOf<SourceToString> {
    
        [TestMethod]
        public void should_show_the_Source_name() {

            When.Convert("web", null, null, null)
                .ShouldBe("via web");

            When.Convert("<a href=\"http:\a.com\">source name</a>", null, null, null)
                 .ShouldBe("via source name");
                 
            When.Convert("&lt;a href=&quot;http://echofon.com/&quot; rel=&quot;nofollow&quot;&gt;Echofon&lt;/a&gt;", null, null, null)
                 .ShouldBe("via Echofon");

            When.Convert("unknown source", null, null, null)
                .ShouldBe("via unknown");
        }

    }
}