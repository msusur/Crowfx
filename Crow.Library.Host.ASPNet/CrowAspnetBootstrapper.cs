using System.Web;
using Crow.Library.Host.AspNet;

[assembly: PreApplicationStartMethod(typeof(CrowAspnetBootstrapper), "Start")]
namespace Crow.Library.Host.AspNet
{
    /// <summary>
    /// Initializes the application when the Asp.Net Host started.
    /// </summary>
    public class CrowAspnetBootstrapper
    {
        private static bool _ApplicationInitialized;
        private static object _syncRoot = new object();
        private static Bootstrappers.Bootstrapper _boostrapper;
        private static readonly AspNetApplicationLifeManager _lifeManager
            = new AspNetApplicationLifeManager(OnApplicationShutdown, OnApplicationStart);

        /// <summary>
        /// Starts the application. This method executes by the <see cref="PreApplicationStartMethod"/>.
        /// </summary>
        public static void Start()
        {
            if (!_ApplicationInitialized)
            {
                lock (_syncRoot)
                {
                    if (!_ApplicationInitialized)
                    {
                        _lifeManager.InitializeManager();
                        _ApplicationInitialized = true;

                    }
                }
            }
        }

        private static void OnApplicationStart()
        {
            _boostrapper = new CrowHostBootstrapper();
            _boostrapper.Start();
        }

        private static void OnApplicationShutdown()
        {
            _boostrapper.Stop();
        }
    }
}
