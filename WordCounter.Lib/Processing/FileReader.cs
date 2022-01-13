namespace WordCounter.Lib.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using WordCounter.Lib.Events;

    internal class FileReader : IDisposable
    {
        private readonly string _path;
        private readonly StringBuilder _stringBuilder;
        private readonly Dictionary<string, int> _dictionary;
        private readonly BackgroundWorker _backgroundWorker;
        private int _previousProgressPercentage;
        private string _error;

        public WorkerFinishedEvent WorkFinishedEvent;
        public WorkerProgressChangedEvent WorkProgressChangedEvent;

        public FileReader(string path)
        {
            _path = path;
            _stringBuilder = new StringBuilder();
            _dictionary = new Dictionary<string, int>();
            _backgroundWorker = new BackgroundWorker();
            WorkProgressChangedEvent = new WorkerProgressChangedEvent();
            WorkFinishedEvent = new WorkerFinishedEvent();

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += ReadByBytes;
            _backgroundWorker.RunWorkerCompleted += WorkerCompleted;
            _backgroundWorker.ProgressChanged += ProgressChanged;
        }

        public void StartWorker()
        {
            if (_backgroundWorker != null && !_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        public void CancelWorker()
        {
            if (_backgroundWorker != null && _backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
            }
        }

        #region BackgroundWorker Events
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;

            WorkProgressChangedEvent.OnWorkerProcessChanged(new WorkerProgressChangedEventArgs(progress));
            _previousProgressPercentage = progress;
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string message = "Done!";
            ObservableCollection<KeyValuePair<string, int>> list = null;

            if (!string.IsNullOrEmpty(_error))
            {
                message = _error;
            }
            else if (e.Cancelled)
            {
                message = "Process cancelled!";
            }
            else
            {
                var sorted = Sorter.Sort(_dictionary);
                list = new ObservableCollection<KeyValuePair<string, int>>(sorted);
                WorkProgressChangedEvent.OnWorkerProcessChanged(new WorkerProgressChangedEventArgs(100));
            }

            WorkFinishedEvent.OnWorkerFinished(new WorkerFinishedEventArgs(message, list));
        }

        private void ReadByBytes(object sender, DoWorkEventArgs e)
        {
            if (!PathIsValid())
            {
                return;
            }

            using var reader = new StreamReader(_path);

            long fileSize = reader.BaseStream.Length;
            int readBytes = 0;

            int byteValue = reader.Peek();

            while (byteValue >= 0 && !_backgroundWorker.CancellationPending)
            {
                byteValue = reader.Read();
                HandleCharacterFromByteValue(byteValue);

                readBytes++;

                int progressPercentage = (int)((double)readBytes / fileSize * 100);
                if (progressPercentage != _previousProgressPercentage)
                {
                    _backgroundWorker.ReportProgress(progressPercentage);
                    _previousProgressPercentage = progressPercentage;
                }
            }

            if (_backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private bool PathIsValid()
        {
            _error = "";

            if (string.IsNullOrEmpty(_path))
            {
                _error = "No path given!";
                return false;
            }

            if (!File.Exists(_path))
            {
                _error = "File cannot be found!";
                return false;
            }

            var info = new FileInfo(_path);
            if (info.Length == 0)
            {
                _error = "The file is empty!";
                return false;
            }

            return true;
        }
        #endregion

        #region BackgroundWorker Helpers
        private void HandleCharacterFromByteValue(int byteValue)
        {
            char character = (char)byteValue;

            if (char.IsWhiteSpace(character))
            {
                AddNewWordIfNotEmpty();
            }
            else
            {
                _stringBuilder.Append(character);
            }
        }

        private void AddNewWordIfNotEmpty()
        {
            if (_stringBuilder.Length > 0)
            {
                string word = _stringBuilder.ToString();

                AddOrIncreaseWordOccurance(word);

                _stringBuilder.Clear();
            }
        }

        private void AddOrIncreaseWordOccurance(string word)
        {
            if (!_dictionary.ContainsKey(word))
            {
                _dictionary[word] = 0;
            }

            _dictionary[word]++;
        }

        public void Dispose()
        {
            _backgroundWorker.DoWork -= ReadByBytes;
            _backgroundWorker.RunWorkerCompleted -= WorkerCompleted;
            _backgroundWorker.ProgressChanged -= ProgressChanged;
        }
        #endregion
    }
}