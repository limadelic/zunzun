using System;
using System.Security.Cryptography;
using System.Text;

namespace Zunzun.Utils.Classes {

    public class KeyMakerClass : KeyMaker {

        static readonly byte[] Entropy = Encoding.Unicode.GetBytes("and now for something completely different");

        public string Encrypt(string Msg) { return 
            Convert.ToBase64String(
                ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(Msg), 
                    Entropy,
                    DataProtectionScope.CurrentUser
        ));}

        public string Decrypt(string Msg) { return 
            Encoding.Unicode.GetString(
                ProtectedData.Unprotect(
                    Convert.FromBase64String(Msg), 
                    Entropy,
                    DataProtectionScope.CurrentUser
        ));}
    }
}