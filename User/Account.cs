namespace User
{
    public class Account
    {
        //Account constructor voor de register
        public Account(string username, string password, string email, Role role)
        {
            _username = username;
            _password = password;
            _email = email;
            _role = role;
        }

        private int _customerid;  // misschien nog een 3e constructor met customerid, voor het beheren van accounts
        public int CustomerId { get {  return _customerid; } }

        private string _username;
        public string Username { get { return _username;  } }

        private string _password;
        public string Password { get { return _password; } }

        private string _email;
        public string Email { get { return _email; } }

        private Role _role;
        public Role Role { get { return _role; } }
    }
}