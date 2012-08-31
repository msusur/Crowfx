using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Tests.Aspects
{
    public interface ITestBusiness
    {
        [Aspect_Tests.TestAspect]
        TestData GetTestDataWithAspect(int i);
        [Aspect_Tests.TestAspect]
        TestData GetTestDataWithoutAspect(int i);
        TestData GetTestDataWithAspectOnConcreteClass(int i);
        [Aspect_Tests.TestAspect(Order = 1)]
        [Aspect_Tests.TestAspect2(Order = 2)]
        TestData MultipleAspect(int i);
    }
}
