using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public User(string UserName, string password)
        {
            Email = UserName;
            Password = password;
        }

        public List<Email> AllEmails { get; set; }

    }
}
