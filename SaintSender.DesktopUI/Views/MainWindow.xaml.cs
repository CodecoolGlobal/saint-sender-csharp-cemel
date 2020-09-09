using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using OpenPop.Mime;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;
using SaintSender.DesktopUI.Views;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _vm = new MainViewModel();
        private ObservableCollection<Email> emailToSHow;
        private List<Email> allEmails;
        private DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            _vm.SetupEmails();
            emailToSHow = _vm.BuildUpEmailsToShow();
            allEmails = emailToSHow.ToList<Email>();
            DataContext = this;
            //SaintSender.Core.Services.Sender.SendEmail("lilaalex95@gmail.com", "okeka", "neeeeeecsinald");

            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public ObservableCollection<Email> EmailsToDisplay
        {
            get { return emailToSHow; }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (txtSearch.Text == null || txtSearch.Text == "")
            {
                RefreshEmailList();
            }
        }

        private void RefreshEmailList()
        {
            _vm.SetupEmails();
            emailToSHow = _vm.BuildUpEmailsToShow();
            allEmails = emailToSHow.ToList<Email>();
        }

        private void writeMail_Click(object sender, RoutedEventArgs e)
        {
            WriteMail wm = new WriteMail();
            wm.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchInEmails(txtSearch.Text);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchInEmails(txtSearch.Text);
        }

        private void SearchInEmails(string text)
        {
            string pattern = ".*";

            if (text != null && text != "")
            {
                pattern = $".*{text.ToUpperInvariant()}.*";
            }

            var query = from item in allEmails
                        where (Regex.IsMatch(item.Body.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Date.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Sender.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Subject.ToUpperInvariant(), pattern))
                        select item;

            ObservableCollection<Email> bob = new ObservableCollection<Email>(query);
            emailToSHow.Clear();

            foreach (var item in bob)
            {
                emailToSHow.Add(item);
            }
        }
    }
}
