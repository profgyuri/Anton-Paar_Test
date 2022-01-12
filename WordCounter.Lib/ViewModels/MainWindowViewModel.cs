namespace WordCounter.Lib.ViewModels
{
    using System.Windows.Input;
    using WordCounter.Lib.Processing;

    public class MainWindowViewModel : ViewModelBase
    {
        private bool _startButtonEnabled;
        private string _filePath;
        private FileReader _fileReader;

        public bool StartButtonEnabled
        {
            get => _startButtonEnabled;
            set
            {
                _startButtonEnabled = value;
                OnPropertyChanged(nameof(StartButtonEnabled));
                OnPropertyChanged(nameof(CancelButtonEnabled));
            }
        }

        public bool CancelButtonEnabled
        {
            get => !_startButtonEnabled;
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public ICommand StartCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public MainWindowViewModel()
        {
            StartButtonEnabled = true;

            StartCommand = new RelayCommand(() =>
            {
                _startButtonEnabled = false;
                _fileReader = new FileReader(_filePath);
                _fileReader.StartWorker();
            });
            CancelCommand = new RelayCommand(() =>
            {
                _startButtonEnabled = true;
                _fileReader?.CancelWorker();
            });
        }
    }
}
