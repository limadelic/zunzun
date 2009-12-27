using System;
using PostSharp.Aspects;

namespace Zunzun.Utils.Aspects {

    [Serializable]
    public abstract class LocationInterceptionAspectBase : LocationInterceptionAspect {
        
        [NonSerialized]
        protected LocationInterceptionArgs Args;

        public virtual object Value {
            get { return Args.Value; } 
            set { Args.Value = value;}
        }
        
        public virtual void GetValue() { base.OnGetValue(Args); }

        public virtual void SetValue() { base.OnSetValue(Args); }
    }
}