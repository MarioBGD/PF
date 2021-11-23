using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PF.App.Core.LibUI
{
    public class SimpleCommand : ICommand
    {
        private readonly bool _async;
        private readonly Func<Task> _task;
        private readonly Action _action;
        
        public SimpleCommand(Func<Task> task)
        {
            _async = true;
            _task = task;
        }
        
        public SimpleCommand(Action action)
        {
            _async = false;
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (_async)
            {
                await _task.Invoke();
            }
            else
            {
                _action?.Invoke();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}