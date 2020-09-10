using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    public class Window10ViewModel : INotifyPropertyChanged
    {
        private string _s;
        public string Search
        {
            get { return _s; }
            set { _s = value; NotifyPropertyChanged(); GetResults(_s); }
        }

        private readonly ObservableCollection<string> _results = new ObservableCollection<string>();
        public ObservableCollection<string> Results { get { return _results; } }

        private void GetResults(string s)
        {
            _results.Clear();
            Task.Factory.StartNew(() =>
            {
                //perform the actual search here and return a list with the results...
                return new List<string> { "1", "2", "3" };
            }).ContinueWith(task =>
            {
                //add the results to the source collection
                foreach (string result in task.Result)
                    _results.Add(result);

            }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
