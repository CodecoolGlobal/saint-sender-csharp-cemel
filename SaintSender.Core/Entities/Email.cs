using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    public class Email
    {
        public Email(string sender, string date, string subject, string body)
        {
            Sender = sender;
            Date = date;
            Subject = subject;
            Body = body;
            //ID = id;
        }



        public string Sender
        {
            get;
            set;
        }

        public string Date { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int ID { get; set; }
    }
}
