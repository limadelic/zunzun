using Zunzun.Utils.Classes;

namespace Zunzun.Utils {

    public class ObjectFactory {
    
        public static KeyMaker NewKeyMaker { get { return new KeyMakerClass(); } }
        public static WebRequest NewWebRequest { get { return new WebRequestClass(); } }
        public static WebRequestContent NewWebRequestContent { get { return new WebRequestContentClass(); } }
    }
}