using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zunzun.Utils;
using Zunzun.Utils.Aspects;

namespace Zunzun.Specs {

    [TestClass]
    public class when_encrypting_values : BehaviorOf<EncryptedAttribute> {

        const string Value = "Value";
        readonly KeyMaker KeyMaker = TestObjectFor<KeyMaker>();
        
        [TestInitialize]
        public void SetUp() {
            Given.Value = Value;
            Given.KeyMaker.Is(KeyMaker);
        }

        [TestMethod]
        public void should_encrypt_on_set() {

            KeyMaker.Given().Encrypt(Value).Is("Encrypted Value");
            When.OnSetValue(null);
            Should.Value = "Encrypted Value";
        }
        
        [TestMethod]
        public void should_not_encrypt_nulls() {

            Given.Value = null;
            When.OnSetValue(null);
            KeyMaker.ShouldNot().IgnoringArgs().Encrypt(null);
        }

        [TestMethod]
        public void should_decrypt_on_get() {
            
            KeyMaker.Given().Decrypt(Value).Is("Decrypted Value");
            When.OnGetValue(null);
            Should.Value = "Decrypted Value";
        }

        [TestMethod]
        public void should_not_decrypt_nulls() {

            Given.Value = null;
            When.OnGetValue(null);
            KeyMaker.ShouldNot().IgnoringArgs().Decrypt(null);
        }
    }
}