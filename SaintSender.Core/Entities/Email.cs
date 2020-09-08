using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    public class Email
    {
        public String Sender
        {
            get;
            set;
        }

        public DateTime Date { get; set; }

        public String Subject { get; set; }

        public String Body { get; set; }

        public int ID { get; set; }
    }
}
