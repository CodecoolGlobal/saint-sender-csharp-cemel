using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            IList<String> rawMessages = new List<String>();
            foreach (var mail in messages)
            {
                var mailId = mail.Id;
                var request = service.Users.Messages.Get("me", mailId);
                request.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Raw;
                var message = request.Execute();
                //Debug.Write(DecodeBase64String(message.Raw));
                //rawMessages.Add(DecodeBase64String(message.Raw));
                Debug.Write(message.Raw);
                rawMessages.Add(message.Raw);
            }
            return rawMessages;
        }
        //static String DecodeBase64String(string s)
        //{
        //    var ts = s.Replace("-", "+");
        //    ts = ts.Replace("_", "/");
        //    var bc = Convert.FromBase64String(ts);
        //    var tts = Encoding.UTF8.GetString(bc);

        //    return tts;
        //}
    }
}
