using SaintSender.Core.Entities;
using SaintSender.DesktopUI.ViewModels;
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
using System.Windows.Shapes;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ObservableCollection<Email> emailToSHow;
        private MainViewModel _vm = new MainViewModel();
        public Window1()
        {
            InitializeComponent();
            emailToSHow = _vm.ReadOutFromFile();
            DataContext = this;
        }
    }
}
