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
        private Pop3Client _client;
        private List<Message> _receivedEmails;
        public ObservableCollection<Email> _emailsToDisplay;

        public void GetEmails()
        {
            SetupClient();
            _receivedEmails = new List<Message>();
            int messageCount = _client.GetMessageCount();

            for (int i = 1; i <= messageCount; i++)
            {
                _receivedEmails.Add(_client.GetMessage(i));
            }
        }

        public void SetupClient()
        {
            _client = new Pop3Client();
            _client.Connect("pop.gmail.com", 995, true);
            _client.Authenticate("csharptw5@gmail.com", "Csharp123", AuthenticationMethod.UsernameAndPassword);
        }

        #region GetMessageBodies() code
        //public List<string> GetMessageBodies()
        //{
        //    var messageBodies = new List<string>();

        //    foreach (Message message in receivedEmails)
        //    {
        //        MessagePart plainText = message.FindFirstPlainTextVersion();

        //        if (plainText != null)
        //        {
        //            messageBodies.Add(plainText.GetBodyAsText());
        //        }
        //        else
        //        {
        //            MessagePart html = message.FindFirstHtmlVersion();

        //            if (html != null)
        //            {
        //                messageBodies.Add(html.GetBodyAsText());
        //            }
        //        }
        //    }

        //    return messageBodies;
        //}
        #endregion

        public ObservableCollection<Email> BuildUpEmailsToDisplay()
        {
            _emailsToDisplay = new ObservableCollection<Email>();
            foreach (Message message in _receivedEmails)
            {
                if (message.Headers.From.Address != "csharptw5@gmail.com")
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
                        _emailsToDisplay.Add(new Email(message.Headers.ReturnPath.Address, message.Headers.Date, message.Headers.Subject, body));
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }

            return _emailsToDisplay;
        }
    }
}
