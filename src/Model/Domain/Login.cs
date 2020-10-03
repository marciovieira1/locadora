using System;

namespace Locadora.Domain
{
    public class Login
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String ReturnUrl { get; set; }
        public Boolean NeedsToBeActive { get; set; }

        public Login()
        {
        }

        public Login(string username, string password, bool active)
        {
            this.Username = username;
            this.Password = password;
            this.NeedsToBeActive = active;
        }
    }
}
