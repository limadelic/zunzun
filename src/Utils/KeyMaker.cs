namespace Zunzun.Utils {

    public interface KeyMaker {
    
        string Encrypt(string Password);
        string Decrypt(string Password);
    }
}