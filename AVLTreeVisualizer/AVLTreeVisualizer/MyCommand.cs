using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AVLTreeVisualizer
{
    public class MyCommand : ICommand
    {
        private Func<object, bool> canExecute;
        private Action<object> execute;

        public MyCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
