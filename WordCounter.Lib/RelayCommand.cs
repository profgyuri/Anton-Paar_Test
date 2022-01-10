namespace WordCounter.Lib
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        public RelayCommand(Action command)
        {
            Action = command;
        }

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        protected Action Action { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            Action();
        }
    }
}
