using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.NetworkInformation;
using SaintSender.Core.Entities;
using OpenPop.Mime;
using OpenPop.Pop3;


namespace SaintSender.DesktopUI.ViewModels
{
    class MainViewModel
    {
        private List<Message> receivedEMails;
        private ObservableCollection<Email> emailToSHow;

        public List<Message> getEmails()
        {
            List<Message> finalMessages = new List<Message>();
            Pop3Client client = new Pop3Client();
            client.Connect("pop.gmail.com", 995, true);
            client.Authenticate("csharptw5@gmail.com", "Csharp123", AuthenticationMethod.UsernameAndPassword);
            int messageCount = client.GetMessageCount();
            var e = client.GetMessage[0];

            for(int i = messageCount;i < 0; i--)
            {
                finalMessages.Add(client.GetMessage(i));
            }
            return finalMessages;   
        }

 

    }
}
