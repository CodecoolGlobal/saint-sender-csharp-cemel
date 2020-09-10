﻿using OpenPop.Pop3.Exceptions;
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
    public partial class Login : Window
    {
        MainViewModel _vm;

        public Login()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            txtEmail.Focus();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (TryLogin(txtEmail.Text, txtPassword.Password))
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

        private bool TryLogin(string userName, string password)
        {
            txtInfo.Foreground = Brushes.Gray;
            txtInfo.Text = "Please wait...";
            try
            {
                _vm.SetupClient(userName, password);
            }
            catch (InvalidLoginException e)
            {
                //MessageBox.Show("Invalid Email or Password.");
                txtInfo.Foreground = Brushes.Red;
                txtInfo.Text = "Invalid Email or Password.";
                txtPassword.Password = "";
                txtPassword.Focus();

                return false;
            }

            return true;
        }
    }
}
