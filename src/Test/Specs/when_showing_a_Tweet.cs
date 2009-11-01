using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Converters;

namespace Zunzun.Specs {
    
    [TestClass]
    public class when_showing_a_Tweet : BehaviorOf<SourceToString> {
       
       [TestMethod]
       public void should_show_the_Source_name() {
           
           When.Convert("source name", null, null, null)
               .ShouldBe("via source name");
               
           When.Convert("<a href=\"http:\a.com\">source name</a>", null, null, null)
                .ShouldBe("via source name");
       }
    }
}