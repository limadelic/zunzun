namespace Zunzun.Domain {

    public interface UserService {
    
        User FindByUserName(string UserName);
        void Follow(string UserName);
    }
}