using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Helpers;
using Crow.Library.Host.Conventions;

namespace Crow.Library.Host.Controllers.BusinessController
{
    internal class CrowBusinessController : BusinessControllerBase
    {
        public CrowBusinessController(object businessInstance, UrlParser url, INamingConvention convention)
            : base(businessInstance, url, convention)
        {

        }
    }
}
