using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace Crow.Library.Host.AspNet.Handlers
{
    internal abstract class AsyncHandlerBase : IHttpAsyncHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public abstract Task ProcessRequestAsync(HttpContextBase context);

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            Task task = ProcessRequestAsync(new HttpContextWrapper(context));
            var retVal = new AsyncTaskResult(task, extraData);

            if (task == null)
            {
                return null;
            }

            if (cb != null)
            {
                task.ContinueWith(_ => cb(retVal));
            }

            return retVal;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
