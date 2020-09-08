using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SaintSender.Core.Services
{
    public class DataHandler
    {
        public IList<string> getMessageBody()
        {
            var service = Setup.Init();
            // Define parameters of request.
            //UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");
            UsersResource.MessagesResource.ListRequest request1 = service.Users.Messages.List("me");

            //get our emails   

            IList<Message> messages = request1.Execute().Messages;
            IList<String> snippets = new List<String>();
            foreach (var mail in messages)
            {
                var mailId = mail.Id;
                var threadId = mail.ThreadId;

                Message message = service.Users.Messages.Get("me", mailId).Execute();
                snippets.Add(message.Snippet);
            }
            return snippets;
        }
    }
}
