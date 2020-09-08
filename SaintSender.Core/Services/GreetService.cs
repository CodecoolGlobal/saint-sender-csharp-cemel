using System;
using SaintSender.Core.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    public class GreetService
    {
        public IList<String> Greet(string name)
        {
        // return $"Welcome {name}, my friend!";

        string[] Scopes = { GmailService.Scope.GmailReadonly };
        string ApplicationName = "Gmail API .NET Quickstart";

            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

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
