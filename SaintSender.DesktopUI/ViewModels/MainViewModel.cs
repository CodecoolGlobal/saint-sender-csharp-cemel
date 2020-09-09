using OpenPop.Mime;
using OpenPop.Pop3;
using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;


namespace SaintSender.DesktopUI.ViewModels
{
    class MainViewModel
    {
        private List<Message> receivedEmails;
        public ObservableCollection<Email> emailToShow = new ObservableCollection<Email>();

        public List<Message> GetEmails()
        {
            Pop3Client client = new Pop3Client();
            client.Connect("pop.gmail.com", 995, true);
            client.Authenticate("csharptw5@gmail.com", "Csharp123", AuthenticationMethod.UsernameAndPassword);

            List<Message> messages = new List<Message>();
            int messageCount = client.GetMessageCount();


            for(int i = 1;i < messageCount; i++)
            {
                messages.Add(client.GetMessage(i));
            }

            return messages;
        }

        public void SetupEmails()
        {
            receivedEmails = GetEmails();
        }

        public List<string> GetMessageBodies()
        {
            var messageBodies = new List<string>();

            foreach (Message message in receivedEmails)
            {
                MessagePart plainText = message.FindFirstPlainTextVersion();

                if (plainText != null)
                {
                    messageBodies.Add(plainText.GetBodyAsText());
                }
                else
                {
                    MessagePart html = message.FindFirstHtmlVersion();

                    if (html != null)
                    {
                        messageBodies.Add(html.GetBodyAsText());
                    }
                }
            }

            return messageBodies;
        }

        public ObservableCollection<Email> BuildUpEmailsToShow()
        {
            foreach (Message message in receivedEmails)
            {
                if(message.Headers.From.Address != "csharptw5@gmail.com")
                {

                    MessagePart messagePart = message.FindFirstPlainTextVersion();
                    string body;

                    if (!message.MessagePart.IsMultiPart)
                    {
                        body = message.MessagePart.GetBodyAsText();
                    }
                    else
                    {
                        body = messagePart.GetBodyAsText();
                    }

                    try
                    {
                        emailToShow.Add(new Email(message.Headers.ReturnPath.Address, message.Headers.Date, message.Headers.Subject, body));
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
            return emailToShow;

        }
    }
}
