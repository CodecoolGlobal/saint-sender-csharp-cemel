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

namespace SaintSender.DesktopUI.ViewModels
{
    public class LoginViewModel 
    {
        private String _username;
        private String _password;

        public RelayCommand ButtonSignInClick { get; set; }
 
        public String UserName
        {
            get { return _username; }
        }
        
        public String Password
        {
            get { return _password; }
         }

        public LoginViewModel() 
        {
            ButtonSignInClick = new RelayCommand(DisplayInMessageBox,MessageBoxCanUse);
        }
        public void DisplayInMessageBox(object message)
        {
            MessageBox.Show("Working");
        }

        public bool MessageBoxCanUse(object message)
        {
            //if ((string)message == "Im a console") return false;
            return true;
        }






        //public async void ButtonSignInClick(object sender)
        //{
            

        //    //txtInfo.Foreground = Brushes.Gray;
        //    //txtInfo.Text = "Please wait...";
        //    //await Task.Delay(100);

        //    //if (TryLogin(txtEmail.Text, txtPassword.Password))
        //    //{ 
        //    //    MainWindow mw = new MainWindow();
        //    //    mw.Show();
        //    //    this.Close();
        //    //}


        //}

        //private bool TryLogin(string userName, string password)
        //{
        //    try
        //    {
        //        _vm.SetupClient(userName, password);
        //    }
        //    catch (InvalidLoginException e)
        //    {

        //        //MessageBox.Show("Invalid Email or Password.");
        //        txtInfo.Foreground = Brushes.Red;
        //        txtInfo.Text = "Invalid Email or Password.";
        //        Console.WriteLine("Failure: " + e);
        //        txtPassword.Password = "";
        //        txtPassword.Focus();
        //        return false;
        //    }

        //    return true;
        //}
    }
}
