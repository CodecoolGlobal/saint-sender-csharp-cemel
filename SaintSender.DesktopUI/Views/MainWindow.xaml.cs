using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenPop.Mime;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _vm;
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;
        }

        private void GreetBtn_Click(object sender, RoutedEventArgs e)
        {
            //var service = new DataHandler();
            //var emailListResponse = service.getMessageBody();
            //List<Message>l = _vm.getEmails();
            _vm.SetupEmails();
            var messageBodies = _vm.GetMessageBodies();
            ShowMessageBodies(messageBodies);
            _vm.BuildUpEmailsToShow();
        }

        private void ShowMessageBodies(List<string> messageBodies)
        {
            foreach (var messageBody in messageBodies)
            {
                MessageBox.Show(messageBody);
            }
        }
    }
}
