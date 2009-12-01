namespace Zunzun.Specs.Fixtures {

    public class SuiteSetUpFixture {
        
        public string UserName { set { Domain.Settings.UserName = value; } }
        public string Password { set { Domain.Settings.Password = value; } }
    }
}