using System;

namespace MySite.Web.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public  bool RememberMe { get; set; }
    }
}