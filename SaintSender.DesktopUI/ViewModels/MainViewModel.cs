﻿using OpenPop.Mime;
using OpenPop.Pop3;
using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;



namespace SaintSender.DesktopUI.ViewModels
{
    class MainViewModel
    {
        private List<Message> receivedEMails;
        private ObservableCollection<Email> emailToSHow = new ObservableCollection<Email>();

        public List<Message> getEmails()
        {
            List<Message> finalMessages = new List<Message>();
            Pop3Client client = new Pop3Client();
            client.Connect("pop.gmail.com", 995, true);
            client.Authenticate("csharptw5@gmail.com", "Csharp123", AuthenticationMethod.UsernameAndPassword);
            int messageCount = client.GetMessageCount();
            


            for(int i = 1;i < messageCount; i++)
            {
                finalMessages.Add(client.GetMessage(i));
            }
             return finalMessages;   
        }

        public void setupEmails()
        {
            receivedEMails = getEmails();
        }

        public void BuildUpEmailsToShow()
        {

            foreach(Message message in receivedEMails)
            {
                if(message.Headers.From.Address != "csharptw5@gmail.com")
                {

                    string body;
                    MessagePart messagePart = message.FindFirstPlainTextVersion();
                    if (!message.MessagePart.IsMultiPart)
                    {
                        body = message.MessagePart.GetBodyAsText();
                    }
                    else
                    {
                        body = messagePart.GetBodyAsText();
                    }
                
               
                    emailToSHow.Add(new Email(message.Headers.ReturnPath.Address, message.Headers.Date, message.Headers.Subject, body));
                }
            }

        }

        public void checkIfNewMailReceived()
        {

        }


        
    }
}
