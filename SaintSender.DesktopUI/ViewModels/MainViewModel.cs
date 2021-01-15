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
using SaintSender.Core.Services;
using System.Linq;
using System.Windows.Threading;
using SaintSender.DesktopUI.Views;

namespace SaintSender.DesktopUI.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private Pop3Client _client;
        private SetupClient _setupClient;
        private List<Message> _receivedEmails;
        public ObservableCollection<Email> _emailsToDisplay;
        private InternetChecker _internetChecker;
        public User _user;
        public List<Email> _allEmails;
        private bool ConnectionAvailable;
        private DispatcherTimer _timer;
        private DispatcherTimer _timer2;

        public RelayCommand WriteEmailClick { get; set; }
        public bool CanWriteEmail(object message)
        {
            return true;
        }
        
 
        public MainViewModel(string UserName, string Password)
        {
            _internetChecker = new InternetChecker();
             ConnectionAvailable = _internetChecker.CheckForInterNetConnection();
            _setupClient = new SetupClient();
            _client = _setupClient.Setup(UserName, Password);
            _user = new User(UserName, Password);
            InternetSetter();

            WriteEmailClick = new RelayCommand(WriteMail, CanWriteEmail);
        }

        public ObservableCollection<Email> EmailsToDisplay
        {
            get { return _emailsToDisplay; }
            set { _emailsToDisplay = value; OnPropertyChanged(); }
        }

        private void WriteMail(object sender)
        {
            WriteMail wm = new WriteMail();
            wm.Show();
        }

        //private void SetTimer()
        //{
        //    _timer = new DispatcherTimer();
        //    _timer.Interval = new TimeSpan(0, 0, 10);
        //    _timer.Tick += Timer_Tick;
        //    _timer.Start();
        //}

        //private void SetTimer2()
        //{
        //    _timer2 = new DispatcherTimer();
        //    _timer2.Interval = new TimeSpan(0, 0, 1);
        //    _timer2.Tick += Timer_Tick2;
        //    _timer2.Start();
        //}

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtSearch.Text))
        //    {
        //        RefreshEmailList();
        //    }
        //}

        //public void Timer_Tick2(object sender, EventArgs e)
        //{
        //    if (ConnectionAvailable)
        //    {

        //        Internet_Available_Button.Background = Brushes.Green;
        //    }
        //    else
        //    {
        //        Internet_Available_Button.Background = Brushes.Red;
        //    }


        //    if (!boolChangedChecker(InternetChecker.InternetAvailable))
        //    {
        //        InternetSetter();
        //    }
        //}

        //private async void RefreshEmailList()
        //{
        //    await Task.Run(() => _vm.GetEmails());

        //    if (txtSearch.Text == null || txtSearch.Text == "")
        //    {
        //        _emailsToDisplay.Clear();
        //        foreach (var email in _vm.BuildUpEmailsToDisplay())
        //        {
        //            _emailsToDisplay.Add(email);
        //        }
        //        _allEmails = _emailsToDisplay.ToList<Email>();
        //    }
        //}

        public void InternetSetter()
        {
            if (ConnectionAvailable)
            {
                GetEmails();
                _emailsToDisplay = BuildUpEmailsToDisplay();
                _allEmails = _emailsToDisplay.ToList<Email>();
                //SetTimer();   
                //SetTimer2();
            }
            else
            {
                _emailsToDisplay = ReadOutFromFiles();
                if (_emailsToDisplay != null)
                {
                    _allEmails = _emailsToDisplay.ToList<Email>();
                }
            }
        }
        //private bool boolChangedChecker(bool value)
        //{
        //    bool newValue;

        //    try
        //    {

        //        newValue = _internetChecker.CheckForInterNetConnection();
        //    }
        //    catch
        //    {
        //        newValue = false;
        //    }



        //    if (value != newValue)
        //    {
        //        if (ConnectionAvailable == false)
        //        {
        //            ConnectionAvailable = true;
        //        }
        //        else
        //        {
        //            ConnectionAvailable = false;
        //        }

        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}



        public void GetEmails()
        {
            _receivedEmails = new List<Message>();
            int messageCount = _client.GetMessageCount();
            for(int i = messageCount; i > 0; i--)
            {
                _receivedEmails.Add(_client.GetMessage(i));
            }
        }

        public ObservableCollection<Email> BuildUpEmailsToDisplay()
        {
            _emailsToDisplay = new ObservableCollection<Email>();
            int counterID = 0;
            foreach (Message message in _receivedEmails)
            {
                if (message.Headers.From.Address != _user.Email)
                {
                    MessagePart messagePart = message.FindFirstPlainTextVersion();
                    string body;

                    if (!message.MessagePart.IsMultiPart)
                    {
                        
                        body = message.MessagePart.GetBodyAsText();
                    }

                    else if(messagePart == null)
                    {
                        body = " ";
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
