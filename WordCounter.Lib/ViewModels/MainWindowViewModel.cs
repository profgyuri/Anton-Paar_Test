namespace WordCounter.Lib.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;

    public class MainWindowViewModel : ViewModelBase
    {
        private bool _startButtonEnabled;
        private string _filePath;

        public bool StartButtonEnabled
        {
            get => _startButtonEnabled;
            set => _startButtonEnabled = value;
        }

        public bool CancelButtonEnabled
        {
            get => !_startButtonEnabled;
        }

        public string FilePath
        {
            get => _filePath;
            set => _filePath = value;
        }

        public ICommand StartCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public MainWindowViewModel()
        {
            StartButtonEnabled = true;

            StartCommand = new RelayCommand(() => throw new NotImplementedException());
            CancelCommand = new RelayCommand(() => throw new NotImplementedException());
        }
    }
}
