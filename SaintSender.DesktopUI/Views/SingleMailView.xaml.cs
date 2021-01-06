using SaintSender.Core.Entities;
using System.Windows;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for SingleMail.xaml
    /// </summary>
    public partial class SingleMail : Window
    {
        private Email email; 
        public SingleMail(Email email)
        {
            this.email = email;
            InitializeComponent();
            DataContext = email;

        }



    }
}
