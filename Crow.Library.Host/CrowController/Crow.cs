using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Host.CrowController.Models;

namespace Crow.Library.Host.CrowController
{
    internal class CrowDocumentationController : ICrow
    {
        public HtmlDocument Doc()
        {
            return new HtmlDocument();
        }
    }
}
