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
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _textBoxEmailInput;
        private string _textBoxPasswordInput;
        private string _textInfo;
        private Brush _color;
        MainWindowView mainWindowView;
        public RelayCommand ButtonSignInClick { get; set; }

        public Brush TextColor
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged(); }
        }
        public String TextInformation   
        {
            get { return _textInfo; }
            set { _textInfo = value; OnPropertyChanged(); }
        }
        
        public String TextBoxEmailInput
        {
            get  { return _textBoxEmailInput; }
            set { _textBoxEmailInput = value; OnPropertyChanged(); }
        }
        
        public String TextBoxPasswordInput
        {
            get { return _textBoxPasswordInput; }
            set { _textBoxPasswordInput = value;OnPropertyChanged(); }
         }

        public LoginViewModel() 
        {
            ButtonSignInClick = new RelayCommand(SignInClick, SignInCanUse);
        }

        public bool SignInCanUse(object message)
        {
            return true;
        }

        public async void SignInClick(object parameter)
        {
            var passwordBox = (parameter as System.Windows.Controls.PasswordBox);
            var password = passwordBox.Password;
            TextBoxPasswordInput = (string)password;
            passwordBox.Clear();
            TextInformation = "Please wait...";
            TextColor = Brushes.Gray;                  
            await Task.Delay(100);
            if (TryLogin(_textBoxEmailInput, _textBoxPasswordInput))
            {
                ClosingWindow.CloseWindow(this);
            }
        }
        private bool TryLogin(string userName, string password)
        {
            try
            {
                mainWindowView = new MainWindowView(userName, password);
                mainWindowView.Show();
            }
            catch (InvalidLoginException e)
            {
                MessageBox.Show("Signing in was not successful");
                TextBoxPasswordInput = "";
                TextBoxEmailInput = "";
                TextInformation = "";
                return false;
            }
            return true;
        }
    }
}
