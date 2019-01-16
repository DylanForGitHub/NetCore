using System;

namespace MySite.Web.Models
{
    public class RegistionModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public string VerificationCode { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}