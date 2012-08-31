using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.InjectionContainer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crow.Library.Tests.DependencyInjection
{
    [TestClass]
    public class InjectionTests
    {
        public interface IBusiness { void Foo();}
        public class Business : IBusiness { public void Foo() { } }
        public interface IBusinessArgs { }
        public class BusinessArgs : IBusinessArgs
        {
            public BusinessArgs(IBusiness business)
            {

            }
            public void Foo() { }
        }

        [TestMethod]
        public void Can_register_and_get_type()
        {
            DIContainer.DefaultContainer.Bind<IBusiness, Business>();
            IBusiness business = DIContainer.DefaultContainer.Resolve<IBusiness>();
            DIContainer.DefaultContainer.TeardownType<IBusiness>();
            DIContainer.DefaultContainer.Bind<IBusiness>(business);

            var business2 = DIContainer.DefaultContainer.Resolve<IBusiness>();
            Assert.AreSame(business, business2);
        }

        [TestMethod]
        public void Can_register_resolve_type_with_constructor_arguments()
        {
            DIContainer.DefaultContainer.Bind<IBusiness, Business>();
            DIContainer.DefaultContainer.Bind<IBusinessArgs, BusinessArgs>();
            var args = DIContainer.DefaultContainer.Resolve<IBusinessArgs>();
        }


        [TestCleanup]
        public void Cleanup()
        {
            DIContainer.DefaultContainer.TeardownContainer();
        }
    }
}
