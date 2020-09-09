using SaintSender.Core.Entities;
using SaintSender.DesktopUI.ViewModels;
using SaintSender.DesktopUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _vm = new MainViewModel();
        public ObservableCollection<Email> _emailsToDisplay;
        private List<Email> _allEmails;
        private DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _vm.GetEmails();
            _emailsToDisplay = _vm.BuildUpEmailsToDisplay();
            _allEmails = _emailsToDisplay.ToList<Email>();

            SetTimer();
        }

        public ObservableCollection<Email> EmailsToDisplay
        {
            get { return _emailsToDisplay; }
        }

        private void SetTimer()
        {
            _timer.Interval = new TimeSpan(0, 0, 5);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (txtSearch.Text == null || txtSearch.Text == "")
            {
                RefreshEmailList();
            }
        }

        private async void RefreshEmailList()
        {
            await Task.Run(() => _vm.GetEmails());
            if (txtSearch.Text == null || txtSearch.Text == "")
            {
                _emailsToDisplay.Clear();
                foreach (var stuff in _vm.BuildUpEmailsToDisplay())
                {
                    _emailsToDisplay.Add(stuff);
                }

                _allEmails = _emailsToDisplay.ToList<Email>();
            }
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

            var query = from item in _allEmails
                        where (Regex.IsMatch(item.Body.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Date.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Sender.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Subject.ToUpperInvariant(), pattern))
                        select item;

            ObservableCollection<Email> bob = new ObservableCollection<Email>(query);
            _emailsToDisplay.Clear();

            foreach (var item in bob)
            {
                _emailsToDisplay.Add(item);
            }
        }
    }
}
