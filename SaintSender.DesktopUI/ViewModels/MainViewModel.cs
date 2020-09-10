using OpenPop.Mime;
using OpenPop.Pop3;
using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Net;
using System.Security.Policy;

namespace SaintSender.DesktopUI.ViewModels
{
    class MainViewModel
    {
        private Pop3Client _client;
        private List<Message> _receivedEmails;
        public ObservableCollection<Email> _emailsToDisplay;
        private static string _userName;
        private static string _password;
        public static bool InternetAvailable;


        public static void CheckForInterNetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://google.com/generate_204"))
                    {
                        InternetAvailable = true;
                    }
                }
            }
            catch
            {
                InternetAvailable = false;
            }
        }

        public void GetEmails()
        {

            SetupClient();
            _receivedEmails = new List<Message>();
            int messageCount = _client.GetMessageCount();

            for (int i = messageCount; i > 0; i--)
            {
                _receivedEmails.Add(_client.GetMessage(i));
            }
        }

        public void SetupClient(string userName, string password)
        {
            _userName = userName;
            _password = password;
            SetupClient();
        }

        public void SetupClient()
        {
            _client = new Pop3Client();
            _client.Connect("pop.gmail.com", 995, true);
            //_client.Authenticate("csharptw5@gmail.com", "Csharp123", AuthenticationMethod.UsernameAndPassword);
            _client.Authenticate(_userName, _password, AuthenticationMethod.UsernameAndPassword);
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
            int counterID = 0;
            foreach (Message message in _receivedEmails)
            {
                if (message.Headers.From.Address != _userName)
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
                        _emailsToDisplay.Add(new Email(message.Headers.ReturnPath.Address, message.Headers.Date, message.Headers.Subject, body, counterID));
                        counterID++;
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }

            return _emailsToDisplay;

        }

        public void BackUp(ObservableCollection<Email> emailList)
        {
            

            string filePath = @"StoredMail.txt";

            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fileStream, emailList);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fileStream.Close();
            }

        }

        public ObservableCollection<Email> ReadOutFromFiles()
        {
            // Declare the collection reference.
            ObservableCollection<Email> collection = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(@"StoredMail.txt", FileMode.Open);
            
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the collection from the file and
                // assign the reference to the local variable.
                collection = (ObservableCollection<Email>)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                
            }
            finally
            {
                fs.Close();
            }

            // To prove that the table deserialized correctly,
            // display the key/value pairs.
            

            return collection;
        }

    }
}
