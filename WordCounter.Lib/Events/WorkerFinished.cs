namespace WordCounter.Lib.Events
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public sealed class WorkerFinishedEventArgs : EventArgs
    {
        public string Message { get; set; }
        public ObservableCollection<KeyValuePair<string, int>> OrderedList { get; set; }

        public WorkerFinishedEventArgs(string message, ObservableCollection<KeyValuePair<string, int>> orderedList)
        {
            Message = message;
            OrderedList = orderedList;
        }
    }

    public class WorkerFinishedEvent
    {
        public delegate void WorkerFinishedEventHandler(object o, WorkerFinishedEventArgs e);

        public event WorkerFinishedEventHandler WorkerFinished;

        public virtual void OnWorkerFinished(WorkerFinishedEventArgs e)
        {
            WorkerFinished(this, e);
        }
    }
}