using System.Collections.Generic;
using Crow.Library.Aspects.Attributes;
using Crow.Library.BusinessFactory;
using Crow.Library.Common;
using Crow.Library.Foundation.Common.Aspects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crow.Library.InjectionContainer;
using System.Reflection;
using Crow.Library.Bootstrappers;

namespace Crow.Library.Tests.Aspects
{
    [TestClass]
    public class Aspect_Tests
    {
        internal static Stack<string> _aspectExecutionStack = new Stack<string>();
        public class TestAspect2Attribute : AspectAttributeBase
        {
            [WorksBefore]
            public void BeforeAspect(IMethodInvocationContext context)
            {
                _aspectExecutionStack.Push("BeforeAspect2");
            }

            [WorksAfter]
            public void AfterAspect(IMethodInvocationContext context)
            {
                _aspectExecutionStack.Push("AfterAspect2");
            }
        }
        public class TestAspectAttribute : AspectAttributeBase
        {
            [WorksBefore]
            public void BeforeAspect(IMethodInvocationContext context)
            {
                _aspectExecutionStack.Push("BeforeAspect");
            }

            [WorksAfter]
            public void AfterAspect(IMethodInvocationContext context)
            {
                _aspectExecutionStack.Push("AfterAspect");
            }
        }
        [TestInitialize]
        public void Initialize()
        {
            //CrowCore.RegisterOnDemand(new MyAspectStartupInstaller());
            Bootstrapper boot = new Bootstrapper();
            boot.Start(this.GetType().Assembly);
        }

        [TestMethod]
        public void Users_can_add_an_aspect_to_their_business_library_interfaces()
        {
            ITestBusiness business = Business.Get<ITestBusiness>();
            TestData dt = business.GetTestDataWithAspect(5);
            Assert.AreEqual<int>(5, dt.Id);
            Assert.AreEqual<string>("AfterAspect", _aspectExecutionStack.Pop());
            Assert.AreEqual<string>("BeforeAspect", _aspectExecutionStack.Pop());
        }

        [TestMethod]
        [Ignore]
        ///This Test should be considered again.
        public void Users_can_add_an_aspect_to_their_business_library_classes()
        {
            ITestBusiness business = Business.Get<ITestBusiness>();
            TestData dt = business.GetTestDataWithAspectOnConcreteClass(5);
            Assert.AreEqual<int>(5, dt.Id);
            Assert.AreEqual<string>("AfterAspect", _aspectExecutionStack.Pop());
            Assert.AreEqual<string>("BeforeAspect", _aspectExecutionStack.Pop());
        }

        [TestMethod]
        public void Users_can_ignore_any_aspect_by_just_implementing_the_method_non_virtual()
        {
            ITestBusiness business = Business.Get<ITestBusiness>();
            TestData dt = business.GetTestDataWithoutAspect(5);
            Assert.AreEqual<int>(5, dt.Id);
            Assert.AreEqual<int>(0, _aspectExecutionStack.Count);
        }

        [TestMethod]
        public void Users_can_add_more_than_one_aspects_to_the_methods()
        {
            ITestBusiness business = Business.Get<ITestBusiness>();
            TestData dt = business.MultipleAspect(5);
            Assert.AreEqual<int>(5, dt.Id);
            Assert.AreEqual<string>("AfterAspect2", _aspectExecutionStack.Pop());
            Assert.AreEqual<string>("AfterAspect", _aspectExecutionStack.Pop());
            Assert.AreEqual<string>("BeforeAspect2", _aspectExecutionStack.Pop());
            Assert.AreEqual<string>("BeforeAspect", _aspectExecutionStack.Pop());
        }

        [TestCleanup]
        public void Clean()
        {
            _aspectExecutionStack = new Stack<string>();
            DIContainer.DefaultContainer.TeardownType<ITestBusiness>();
        }
    }
}
