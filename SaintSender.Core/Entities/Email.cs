using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    [Serializable]
    public class Email
    {
        public User UserAccount { get; set; }
        public string Sender { get; set; }

        public string Date { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int ID { get; set; }
        public Email(string sender, string date, string subject, string body, int id)
        {
            Sender = sender;
            Date = date;
            Subject = subject;
            Body = body;
            ID = id;
        }
    }
}
