namespace WordCounter.Lib.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
using System.Runtime.CompilerServices;
    using System.Text;

    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
