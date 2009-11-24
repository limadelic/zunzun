using FluentSpec;
using System.Collections.Generic;
using System.Linq;
using Zunzun.Domain;
using ObjectFactory=Zunzun.Domain.ObjectFactory;

namespace Zunzun.Specs.Fixtures {

    public class FollowAndUnfollow : Spec {
    
        readonly UserService UserService = ObjectFactory.NewUserService;
    
        protected override void SetUpSteps() {

            When("I follow {0}", UserName => UserService.Follow(UserName));

            Then("{0} should be among the people I follow", UserName => 
                Following.Any(User => User.UserName.Equals(UserName)).ShouldBeTrue());
        }

        protected List<User> Following { get { return null; } }
    }
}