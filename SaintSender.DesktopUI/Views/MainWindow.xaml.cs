using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _vm = new MainViewModel();
        private ObservableCollection<Email> emailToSHow;

        public MainWindow()
        {
            InitializeComponent();
            _vm.SetupEmails();
            emailToSHow = _vm.BuildUpEmailsToShow();
            DataContext = this;
            _vm.BackUp(emailToSHow);
        }


        public ObservableCollection<Email> EmailsToDisplay
        {
            get { return emailToSHow; }
        }

    }
}
