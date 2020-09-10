using SaintSender.Core.Entities;
using SaintSender.DesktopUI.ViewModels;
using SaintSender.DesktopUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private DispatcherTimer _timer;
        private DispatcherTimer _timer2;

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel.CheckForInterNetConnection();
            DataContext = this;
            InternetSetter();

        }


        public void InternetSetter()
        {
            if (MainViewModel.InternetAvailable)
            {
                _vm.GetEmails();
                _emailsToDisplay = _vm.BuildUpEmailsToDisplay();
                _allEmails = _emailsToDisplay.ToList<Email>();
                SetTimer();
                SetTimer2();
            }
            else
            {
                _emailsToDisplay = _vm.ReadOutFromFiles();
                if (_emailsToDisplay != null)
                {
                    _allEmails = _emailsToDisplay.ToList<Email>();
                }
            }
        }

        public void Timer_Tick2(object sender, EventArgs e)
        {   
            if(MainViewModel.InternetAvailable == true)
            {
                InternetAvilableButton.Background = Brushes.Green;
            }
            else
            {
                InternetAvilableButton.Background = Brushes.Red;
            }


            if (!boolChangedChecker(MainViewModel.InternetAvailable))
            {
                InternetSetter();
            }
        }

        private void SetTimer2()
        {
            _timer2 = new DispatcherTimer();
            _timer2.Interval = new TimeSpan(0, 0, 1);
            _timer2.Tick += Timer_Tick2;
            _timer2.Start();
        }



        public ObservableCollection<Email> EmailsToDisplay
        {
            get { return _emailsToDisplay; }
        }

        private void SetTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private async void RefreshEmailList()
        {
            await Task.Run(() => _vm.GetEmails());

            if (txtSearch.Text == null || txtSearch.Text == "")
            {
                _emailsToDisplay.Clear();
                foreach (var email in _vm.BuildUpEmailsToDisplay())
                {
                    _emailsToDisplay.Add(email);
                }
                _allEmails = _emailsToDisplay.ToList<Email>();
            }
        }

        private void SearchInEmails(string text)
        {
            string pattern = ".*";

            if (!string.IsNullOrWhiteSpace(text) && txtSearch.Foreground == Brushes.Black)
            {
                pattern = $".*{text.ToUpperInvariant()}.*";
            }

            var query = from item in _allEmails
                        where (Regex.IsMatch(item.Body.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Date.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Sender.ToUpperInvariant(), pattern)
                            || Regex.IsMatch(item.Subject.ToUpperInvariant(), pattern))
                        select item;

            _emailsToDisplay.Clear();

            foreach (var item in query)
            {
                _emailsToDisplay.Add(item);
            }
        }

        private void AddPlaceholderText()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search...";
                txtSearch.Foreground = Brushes.Gray;
            }
        }

        private void RemovePlaceholderText()
        {
            if (txtSearch.Text == "Search...")
            {
                txtSearch.Text = "";
                txtSearch.Foreground = Brushes.Black;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                RefreshEmailList();
            }

            //if (!boolChangedChecker(MainViewModel.InternetAvailable))
            //{
            //    InternetSetter();
            //}

        }

        private bool boolChangedChecker(bool value)
        {
            bool newValue;

            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://google.com/generate_204"))
                    {
                        newValue = true;
                    }
                }
            }
            catch
            {
                newValue = false;
            }



            if (value != newValue)
            {
                if (MainViewModel.InternetAvailable == false)
                {
                    MainViewModel.InternetAvailable = true;
                }
                else
                {
                    MainViewModel.InternetAvailable = false;
                }

                return false;
            }
            else
            {
                return true;
            }

        }




        private void writeMail_Click(object sender, RoutedEventArgs e)
        {
            WriteMail wm = new WriteMail();
            wm.Show();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchInEmails(txtSearch.Text);
        }

        private void txtSearch_Loaded(object sender, RoutedEventArgs e)
        {
            AddPlaceholderText();
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            AddPlaceholderText();
            SearchInEmails(txtSearch.Text);
        }

        private void Save_Email_To_Disk_Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.BackUp(_emailsToDisplay);
        }


        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            RemovePlaceholderText();
        }
    }
}
