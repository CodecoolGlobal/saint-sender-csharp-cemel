using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SaintSender.DesktopUI.ViewModels
{
    public class WriteEmailViewModel : ViewModelBase
    {
        private User _user;
        private string _textTo;
        private string _textSubject;
        private string _textMessage;
        public RelayCommand ButtonSend { get; set; }
        public RelayCommand CloseWindow { get; set; }

        public WriteEmailViewModel(User user)
        {
            _user = user;
            ButtonSend = new RelayCommand(ButtonSendClick, CanBeSent);
            CloseWindow = new RelayCommand(ButtonCancelClick, CanBeCanceled);
        }


        public String TextTo
        {
            get
            {
                return _textTo;
            }
            set
            {
                _textTo = value; OnPropertyChanged();
            }
        }
        public String TextSubject
        {
            get
            {
                return _textSubject;
            }

            set
            {
                _textSubject = value; OnPropertyChanged();
            }
        }

        public String TextMessage
        {
            get
            {
                return _textMessage;
            }
            set
            {
                _textMessage = value; OnPropertyChanged();
            }
        }


        public User UserInformation
        {
            get { return _user; }
            set
            {
                _user = value; OnPropertyChanged();
            }
        }

        public bool CanBeSent(object sender)
        {
            return true;
        }

        public bool CanBeCanceled(object sender)
        {
            return true;
        }

        public void ButtonSendClick(object sender)
        {

            Sender.Email(_user.Email, _user.Password, _textTo, _textSubject, _textMessage);
            ClosingWindow.CloseWindow(this);
            MessageBox.Show("Email successfully sent", "Email sent");
        } 

        private void ButtonCancelClick(object sender)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to discard your message?", "Discard message", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                ClosingWindow.CloseWindow(this);
            }
        }

    }
}
