using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.Utils.Aspects;

namespace Zunzun.Specs {

    public class given_a_setting_is_user_configurable {
        
        readonly static object Value = new object();
        readonly static object PreviousValue = new object();
        
        [TestClass]
        public class when_its_set : BehaviorOf<UserSettingAttribute> {
        
            [TestMethod]
            public void the_value_should_be_applied() {

                Given.Value = Value;
                When.OnSetValue(null);
                Should.UserSetting = Value;
            }
            
            [TestMethod]
            public void the_value_should_be_saved() {

                When.OnSetValue(null);
                Should.Save();
            }
            
            [TestMethod]
            public void nothing_should_happen_if_the_value_doesnt_change() {

                Given.Value = Value;
                Given.UserSetting = Value;
                
                When.OnSetValue(null);
                
                ShouldNot.IgnoringArgs().UserSetting = null;
                ShouldNot.Save();
            }
        }
        
        [TestClass]
        public class when_its_read : BehaviorOf<UserSettingAttribute> {
            
            [TestMethod]
            public void should_get_previously_stored_value() {

                Given.UserSetting = PreviousValue;
                When.OnGetValue(null);
                Should.Value = PreviousValue;
            }
        }
    }
}