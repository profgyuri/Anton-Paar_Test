namespace WordCounter.Lib.Events
{
    using System;

    public class WorkerProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; set; }

        public WorkerProgressChangedEventArgs(int progress)
        {
            Progress = progress;
        }
    }

    public class WorkerProgressChangedEvent
    {
        public delegate void WorkerProcessChangedEventHandler(object o, WorkerProgressChangedEventArgs e);

        public event WorkerProcessChangedEventHandler WorkerProcessChanged;

        public virtual void OnWorkerProcessChanged(WorkerProgressChangedEventArgs e)
        {
            WorkerProcessChanged(this, e);
        }
    }
}