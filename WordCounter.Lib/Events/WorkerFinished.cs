namespace WordCounter.Lib.Events
{

    using System;

    public class WorkerFinishedEventArgs : EventArgs
    {
        public string Message { get; set; }

        public WorkerFinishedEventArgs(string message)
        {
            Message = message;
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