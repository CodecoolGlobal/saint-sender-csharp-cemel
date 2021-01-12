using OpenPop.Pop3.Exceptions;
using SaintSender.DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginView: Window
    {
        LoginViewModel _lvm;

        public LoginView()
        {
            InitializeComponent();
            _lvm = new LoginViewModel();
            DataContext = _lvm;
            txtEmail.Text = "Type your email";
            txtPassword.Focus();
        }

    }
}
