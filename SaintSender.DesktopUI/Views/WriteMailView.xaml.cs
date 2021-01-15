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
using System.Windows.Shapes;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for WriteMail.xaml
    /// </summary>
    public partial class WriteMailView : Window
    {
        WriteEmailViewModel _wevm; 

        public WriteMailView(User _user)
        {
            InitializeComponent();
            _wevm = new WriteEmailViewModel(_user);
            DataContext = _wevm;
        }

    }
}
