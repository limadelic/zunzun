using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.App.Presenters;
using Zunzun.Domain.Classes;
using Zunzun.Specs.Helpers;

namespace Zunzun.Specs {

    [TestClass]
    public class when_showing_UserHome {
        
        [TestClass]
        public class the_Presenter : BehaviorOf<UserHomePresenter> {
            
            [TestMethod]
            public void should_display_the_User_details() {
                var Zunzun = Actors.Zunzun;
                
                Given.UserService.FindByUserName(Zunzun.UserName).WillReturn(Zunzun);
                When.Show(Zunzun.UserName);
                The.View.User.ShouldBe(Zunzun);
            }
        }
        
        [TestClass]
        public class the_UserService : BehaviorOf<UserServiceClass> {
            
            [TestMethod]
            public void should_retrieve_a_user_by_UserName() {
                const string UserName = Actors.ZunzunUserName;
                var Spec = Actors.ZunzunTestSpec;
                
                Given.UserByUserName(UserName).WillReturn(Spec);
                
                var UserFound = When.FindByUserName(UserName);
                
                UserFound.UserName.ShouldBe(UserName);
            }
        }
    }
}