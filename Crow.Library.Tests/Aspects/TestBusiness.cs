using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Tests.Aspects
{
    public class TestBusiness : ITestBusiness
    {
        public virtual TestData GetTestDataWithAspect(int i)
        {
            return new TestData
            {
                Id = i
            };
        }

        public TestData GetTestDataWithoutAspect(int i)
        {
            return new TestData
            {
                Id = i
            };
        }
        [Aspect_Tests.TestAspect]
        public virtual TestData GetTestDataWithAspectOnConcreteClass(int i)
        {
            return new TestData
            {
                Id = i
            };
        }


        public virtual TestData MultipleAspect(int i)
        {
            return new TestData
            {
                Id = i
            };
        }
    }
}
