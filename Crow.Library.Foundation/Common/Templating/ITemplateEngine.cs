using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Common.Templating
{
    /// <summary>
    /// Represents the templating engine interface.
    /// </summary>
    public interface ITemplateEngine
    {
        /// <summary>
        /// Evaluates the given template string and returns the evaluated text.
        /// </summary>
        /// <param name="templateText">Template text with {propertyName} format.</param>
        /// <param name="context">Context object that has the properties referenced from the <see cref="templateText"/></param>
        /// <returns></returns>
        string EvaluateTemplate(string templateText, object context);
    }
}
