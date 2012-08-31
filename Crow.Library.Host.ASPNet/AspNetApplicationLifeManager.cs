using System;
using System.Web.Hosting;
using Crow.Library.Foundation.Logger;
using System.Diagnostics;

namespace Crow.Library.Host.AspNet
{
    internal class AspNetApplicationLifeManager : IRegisteredObject
    {
        private readonly Action _OnApplicationInitialize;
        private readonly Action _OnApplicationShutdown;
        //private readonly ILog _log;

        public AspNetApplicationLifeManager(/*ILog log,*/ Action onStop)
            : this(/*log, */onStop, () => { })
        {

        }

        public AspNetApplicationLifeManager(/*ILog log,*/ Action onStop, Action onStart)
        {
            _OnApplicationInitialize = onStart;
            _OnApplicationShutdown = onStop;
            //_log = log;
        }

        public void InitializeManager()
        {
            try
            {
                HostingEnvironment.RegisterObject(this);
                _OnApplicationInitialize();
            }
            catch (Exception e)
            {
                //_log.Error("AspNet Host start failed.", e);
                Debug.WriteLine("Aspnet host start failed: '{0}'.".FormatText(e.Message));
            }
        }

        public void Stop(bool immediate)
        {
            try
            {
                _OnApplicationShutdown();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Application stop action failed: '{0}'.".FormatText(e.Message));
            }
            finally
            {
                HostingEnvironment.UnregisterObject(this);
            }
        }
    }
}
