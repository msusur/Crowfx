using System;
using System.ComponentModel.Composition;
using System.Net.Http;
using Crow.Library.Common;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.Foundation.Hosting;
using Crow.Library.Foundation.Logger;
using Crow.Library.Host.Bootstrapper;
using Crow.Library.InjectionContainer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Crow.Library.Host;
using System.Net.Sockets;
using System.Net;

namespace Crow.Library.Tests.Hosting
{
    [TestClass]
    public class HostingTests
    {
        private int port = 8090;
        private string hostname = "localhost";

        #region Fake Types
        public class Foo
        {
            public string Text { get; set; }
            public int Id { get; set; }
        }
        public interface IFakeUserBusiness
        {
            int MultiplyFiveBy(int number);
            Foo GetFooById(int id);
            Foo GetFooByFooAndId(int id, Foo foo);
        }

        public class FakeUserBusiness : IFakeUserBusiness
        {
            public int MultiplyFiveBy(int number)
            {
                return 5 * number;
            }
            public Foo GetFooById(int id)
            {
                return new Foo
                {
                    Id = id,
                    Text = "foooooo"
                };
            }
            public Foo GetFooByFooAndId(int id, Foo foo)
            {
                return new Foo
                {
                    Id = id,
                    Text = foo.Text
                };
            }
        }

        [Export(typeof(IModule))]
        public class HostingTestStartupInstaller : IModule
        {
            public void Install(IInjectionContainer container, IQueryStore queryStore)
            {
                container.Bind<IFakeUserBusiness, FakeUserBusiness>();
            }
        }
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            Bootstrappers.Bootstrapper bootstrapper = new Bootstrappers.Bootstrapper();
            bootstrapper.Start(this.GetType().Assembly, typeof(CrowHost).Assembly);
        }

        [TestMethod]
        public void Starts_the_crow_application_when_host_starts()
        {
            ICrowHttpHost http = CrowHost.ConfigureHost(CrowCore.Container,hostname, port, false);
            ILog log = DIContainer.DefaultContainer.Resolve<ILog>();
            Assert.IsNotNull(log);
            Assert.IsNotNull(http);
        }

        [TestMethod, Ignore] //still implementing.
        public void Starts_the_host_when_host_starts()
        {
            ICrowHttpHost http = CrowHost.ConfigureHost(CrowCore.Container, hostname, port, false).Start();
            var client = new HttpClient()
            {
                BaseAddress = new Uri(string.Format("http://{0}:{1}", hostname, port))
            };
            object result = client.GetStringAsync("crow/doc").Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Hosts_an_business_class_when_configured()
        {
            using (ICrowHttpHost http = CrowHost
                                .ConfigureHost(CrowCore.Container, hostname, 80, false)
                                .Host<IFakeUserBusiness>()
                                .AllowDocumentation()
                                .Start())
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri("http://{0}".FormatText(hostname))
                };
                object result = client.GetStringAsync("IFakeUserBusiness/MultiplyFiveBy?number=4").Result;
                Assert.IsNotNull(result);
                Assert.AreEqual<int>(4 * 5, result.ToString().ToInt32());
            }
        }

        [TestMethod]
        public void Hosts_an_business_class_that_has_a_complex_return_type()
        {
            using (ICrowHttpHost http = CrowHost.ConfigureHost(CrowCore.Container, hostname, port))
            {
                http.Host<IFakeUserBusiness>().AllowDocumentation().Start();
                object result = ClientHelper.Call(hostname, port, "IFakeUserBusiness/GetFooById?id=4");

                Assert.IsNotNull(result);
                Foo foo = JsonConvert.DeserializeObject<Foo>(result.ToString());
                Assert.AreEqual<int>(4, foo.Id);
                Assert.AreEqual<string>("foooooo", foo.Text);
            }
        }

        [TestMethod]
        public void Hosts_the_application_with_the_port_spesified_when_configured()
        {
            using (ICrowHttpHost http = CrowHost.ConfigureHost(CrowCore.Container, hostname, port, false))
            {
                http.Host<IFakeUserBusiness>().AllowDocumentation().Start();
                object result = ClientHelper.Call(hostname, port, "IFakeUserBusiness/MultiplyFiveBy?number=4");
                Assert.IsNotNull(result);
                Assert.AreEqual<int>(4 * 5, result.ToString().ToInt32());
            }
        }

        [TestMethod]//still needs some improvements.
        public void Hosts_the_business_class_that_accepts_a_complex_and_non_complex_parameters_in_and_returns_complex_type()
        {
            using (ICrowHttpHost host = CrowHost.ConfigureHost(CrowCore.Container, hostname, port).Host<IFakeUserBusiness>())
            {
                host.Start();
                object result = ClientHelper.Call(hostname, port, "IFakeUserBusiness/GetFooByFooAndId?id=4", new { foo = new Foo { Text = "foo me under" } });
                string textResult = ClientHelper.ReadFromStreamToString(result as HttpResponseMessage);

                Foo fooResult = JsonConvert.DeserializeObject<Foo>(textResult);
                Assert.AreEqual<string>("foo me under", fooResult.Text);
                Assert.AreEqual<int>(4, fooResult.Id);
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
        }
    }
}