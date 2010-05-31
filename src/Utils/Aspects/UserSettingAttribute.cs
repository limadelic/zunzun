using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using UserSettingsClass = Zunzun.Utils.Properties.Settings;

namespace Zunzun.Utils.Aspects {

    [Serializable]
    [ProvideAspectRole(StandardRoles.DataBinding)]
    [ProvideAspectRole(StandardRoles.Persistence)]
    public class UserSettingAttribute : LocationInterceptionAspectBase {
    
        public UserSettingsClass UserSettings { get { return UserSettingsClass.Default; } }
        readonly Type UserSettingsType = typeof(UserSettingsClass);
    
        public override void OnSetValue(LocationInterceptionArgs Args) { 
            this.Args = Args;
            
            if (Value == UserSetting) return;

            UserSetting = Value;
            Save();
        }

        public override void OnGetValue(LocationInterceptionArgs Args) {
            this.Args = Args;
            
            Value = UserSetting;
        }

        public virtual void Save() { UserSettings.Save(); }

        public virtual object UserSetting {
            get { return UserSettingProperty.GetValue(UserSettings, null); } 
            set { UserSettingProperty.SetValue(UserSettings, value, null); }
        }

        PropertyInfo UserSettingProperty { get { return UserSettingsType.GetProperty(SettingName); } }
        
        string SettingName { get { return Args.Location.PropertyInfo.Name; } }
    }
}