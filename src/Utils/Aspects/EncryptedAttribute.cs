using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

namespace Zunzun.Utils.Aspects {

    [Serializable]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.Persistence)]
    public class EncryptedAttribute : LocationInterceptionAspectBase {

        public virtual KeyMaker KeyMaker { get { return ObjectFactory.NewKeyMaker; } }

        public override void OnGetValue(LocationInterceptionArgs Args) {
            this.Args = Args;
            
            GetValue();

            if (HasAValue) Value = KeyMaker.Decrypt(Value.ToString());
        }

        public override void OnSetValue(LocationInterceptionArgs Args) {
            this.Args = Args;

            if (HasAValue) Value = KeyMaker.Encrypt(Value.ToString());
            
            SetValue();
        }
    }
}