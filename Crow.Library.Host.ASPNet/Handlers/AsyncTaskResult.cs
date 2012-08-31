using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Crow.Library.Host.AspNet.Handlers
{
    public sealed class AsyncTaskResult : IAsyncResult
    {
        public Task Task { get; private set; }

        public object AsyncState { get; private set; }

        public WaitHandle AsyncWaitHandle
        {
            get { return ((IAsyncResult)Task).AsyncWaitHandle; }
        }

        public bool CompletedSynchronously
        {
            get { return ((IAsyncResult)Task).CompletedSynchronously; }

        }

        public bool IsCompleted
        {
            get { return ((IAsyncResult)Task).IsCompleted; }
        }

        public AsyncTaskResult(Task task, object asyncState)
        {
            Task = task;
            AsyncState = asyncState;
        }
    }
}
