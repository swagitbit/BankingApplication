using System.Collections.Generic;

namespace BankingApplication.Classes
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
