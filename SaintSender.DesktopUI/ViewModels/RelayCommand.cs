using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaintSender.DesktopUI.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;


        public RelayCommand(Action<object> execute, Predicate<object> canExeute)
        {
            this._execute = execute;
            this._canExecute = canExeute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null) { }


        public event EventHandler CanExecuteChanged
        {
            //pick up the changes that could affect outcome our canexecute
            add  { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            //will return the value that provides if not null
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
