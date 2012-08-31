using Crow.Library.Bootstrappers;
using Crow.Library.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Collections.Generic;
using Crow.Library.Foundation.Bootstrapper;
using Crow.Library.InjectionContainer;

namespace Crow.Library.Tests.Installers
{
    [TestClass]
    public class InstallerTests
    {
        private static bool _isInstalled = false;

        [Export(typeof(IModule))]
        public class DemoStartupInstallerBaseBase : IModule
        {
            public void Install(IInjectionContainer container, IQueryStore queryStore)
            {
                installerstack.Push("Parent Base");
            }
        }


        [Export(typeof(IModule))]
        [DependsOn(typeof(DemoStartupInstallerBaseBase))]
        public class DemoStartupInstallerBase : IModule
        {
            public void Install(IInjectionContainer container, IQueryStore queryStore)
            {
                installerstack.Push("Parent");
            }
        }

        [DependsOn(typeof(DemoStartupInstallerBase))]
        [Export(typeof(IModule))]
        public class DemoStartupInstaller : IModule
        {
            public void Install(IInjectionContainer container, IQueryStore queryStore)
            {
                _isInstalled = true;
                installerstack.Push("Child");
            }
        }
        [TestInitialize]
        public void Start()
        {
            Bootstrapper boot = new Bootstrapper();
            boot.Start(this.GetType().Assembly);
            _isInstalled = false;
            installerstack.Clear();
        }

        private static Stack<string> installerstack = new Stack<string>();
        [TestMethod]
        public void Install_items_as_a_tree()
        {
            Bootstrapper boot = new Bootstrapper();
            boot.Start(this.GetType().Assembly);
            Assert.AreEqual("Child", installerstack.Pop());
            Assert.AreEqual("Parent", installerstack.Pop());
            Assert.AreEqual("Parent Base", installerstack.Pop());
            Assert.AreEqual(0, installerstack.Count);
        }

        [TestMethod]
        public void Install_when_application_is_started()
        {
            Assert.IsFalse(_isInstalled);
            Bootstrapper boot = new Bootstrapper();
            boot.Start(this.GetType().Assembly);
            Assert.IsTrue(_isInstalled);
        }

        [TestMethod]
        public void Install_when_installer_is_registered()
        {
            var installer = new DemoStartupInstaller();
            Bootstrapper boot = new Bootstrapper();
            boot.Start(this.GetType().Assembly);
            Assert.IsTrue(_isInstalled);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _isInstalled = false;
            installerstack.Clear();
        }
    }
}
