using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SaintSender.Core.Entities;
using OpenPop.Pop3.Exceptions;

namespace SaintSender.DesktopUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        MainViewModel _mvm = new MainViewModel();
        private string _textBoxEmailInput;
        private string _textBoxPasswordInput;

        public RelayCommand<string> _buttonSignInClick { get; set; }
 
        public RelayCommand<string> SubmitLoginButton
        {
            get
            {
                if(_buttonSignInClick == null)
                {
                    _buttonSignInClick = new RelayCommand<string>(SignInClick,SignInCanUse);
                }
                return _buttonSignInClick;
            }
        }


        public String TextBoxEmailInput
        {
            get { return _textBoxEmailInput; }
            set { SetProperty(ref _textBoxEmailInput, value, TextBoxEmailInput); }   // not sure
        }
        
        public String TextBoxPasswordInput
        {
            get { return _textBoxPasswordInput; }
            set { SetProperty(ref _textBoxPasswordInput, value, TextBoxPasswordInput); }
         }

        public LoginViewModel() 
        { 
            
            
        }


        public bool SignInCanUse(object message)
        {
            //if ((string)message == "Im a console") return false;
            return true;
        }


        



        public async void SignInClick()
        {


            //txtInfo.Foreground = Brushes.Gray;
            //txtInfo.Text = "Please wait...";
            await Task.Delay(100);



            if (TryLogin(_textBoxEmailInput, _textBoxPasswordInput))
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                //this.Close();
            }


        }

        private bool TryLogin(string userName, string password)
        {
            try
            {
                _mvm.SetupClient(userName, password);
            }
            catch (InvalidLoginException e)
            {

                //MessageBox.Show("Invalid Email or Password.");
                //txtInfo.Foreground = Brushes.Red;
                //txtInfo.Text = "Invalid Email or Password.";
                //Console.WriteLine("Failure: " + e);
                //txtPassword.Password = "";
                //txtPassword.Focus();
                MessageBox.Show("Signing in was not successful");
                return false;
            }

            return true;
        }
    }
}
