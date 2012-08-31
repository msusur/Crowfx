using Crow.Library.Foundation.Hosting;
using Crow.Library.Host;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crow.Library.Common;

namespace Crow.Library.Tests.Sandbox
{
    [TestClass]
    public class CrowHostConceptual
    {
        interface IUserBusiness
        {

        }
        interface IDataBusiness
        {

        }
        class DataFilter : IRequestFilter
        {

        }
        [TestMethod]
        public void StartCrowHost()
        {
            int port = 8080;
            string baseUrl = "localhost";
            bool isHttps = false;

            //Bundle.Create("UserBusinessBundle")
            //        .Add(typeof(UserBusinessv1), "1.0")
            //        .Add(typeof(UserBusinessv2), "2.0");
            //CrowHost.ConfigureHost().HostAsBundle("UserBusinessBundle").Start();

            //localhost/IUserBusiness/2.0/MethodName?id=2
            ICrowHttpHost host = CrowHost.ConfigureHost(CrowCore.Container, baseUrl, port, isHttps)
                                    .Host<IUserBusiness>()
                                    .Host<IDataBusiness>()
                                    .AddRequestFilter<DataFilter>()
                                    .AllowDocumentation()
                                    .Start();
        }
        //[Api(Version="1.0")]
        //class UserBusinessV1 : IUserBusiness
        //{

        //}
        //[Api(Version = "2.0")]
        //class UserBusinessV2 : IUserBusiness
        //{

        //}

    }
}
