using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaintSender.DesktopUI.ViewModels
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;
        private Action signInClick;
        private Func<object, bool> signInCanUse;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null) throw new NullReferenceException("execute");
            _execute = execute;
            _canExecute = canExecute; 
        }

        public RelayCommand(Action<T> execute, object signInCanUse) : this(execute, null)
        {

        }

        public RelayCommand(Action signInClick, Func<object, bool> signInCanUse)
        {
            this.signInClick = signInClick;
            this.signInCanUse = signInCanUse;
        }

        public event EventHandler CanExecuteChanged
        {
            //pick up the changes that could affect outcome our canexecute
            add  { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            //will return the value that provides if not null
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke((T)parameter);
        }
    }
}
