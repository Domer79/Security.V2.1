using System;
using System.Windows.Input;
using WpfApp_TestSecurity.Annotations;

namespace WpfApp_TestSecurity.Infrastructure
{
    public class RelayCommand : ICommand
    {
        [NotNull] private readonly Action<object> _execute;
        [NotNull] private readonly Func<object, bool> _canExecute;

        public RelayCommand([NotNull] Action<object> execute, [NotNull] Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}