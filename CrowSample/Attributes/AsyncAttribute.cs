using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Aspects.Attributes;
using Crow.Library.Foundation.Common.Aspects;
using System.Threading.Tasks;
using Crow.Library.Foundation.Logger;
using Crow.Library.Common;

namespace ConsoleApplication1.Attributes
{
    public class AsyncAttribute : AspectAttributeBase
    {
        [WorksBefore]
        public void ExecuteAsync(IMethodInvocationContext context)
        {
            Task.Factory.StartNew(() => context.Proceed());
            context.Cancel = true;
        }

        [WorksOnException]
        public void OnException(IMethodInvocationContext context)
        {
            ILog log = CrowCore.LogService;
                //Crow.Library.InjectionContainer.DIContainer.DefaultContainer.Resolve<ILog>();
            log.Error(context.Exception.Message);
        }
    }
}
