﻿using System;
using System.Collections.Generic;
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
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for WriteMail.xaml
    /// </summary>
    public partial class WriteMail : Window
    {
        public WriteMail()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Sender.Email("csharptw5@gmail.com", "Csharp123", txtTo.Text, txtSubject.Text, txtMessage.Text);
            this.Close();
        }
    }
}
