using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Aspects;
using Crow.Library.Foundation.Logger;
using Crow.Library.InjectionContainer;
using Crow.Library.Foundation.Common.Templating;

namespace Crow.Library.Aspects.Attributes
{
    /// <summary>
    /// Handler error aspect. Handles the error and writes it to the ILog error.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HandleErrorAttribute : AspectAttributeBase
    {
        /// <summary>
        /// Gets or sets the Error text.
        /// ex: Error occured at {time} , {message}, {methodName}, {type}.
        /// </summary>
        public string ErrorTextFormat { get; set; }

        /// <summary>
        /// Initializes a new instance of the HandleErrorAttribute.
        /// </summary>
        public HandleErrorAttribute()
        {
            ErrorTextFormat = string.Empty;
        }

        /// <summary>
        /// Executes when the method invocation throws an exception.
        /// "Dynamically use only do not call this method."
        /// </summary>
        [WorksOnException, Obsolete("Dynamically use only do not call this method.", true)]
        public void HandleError(IMethodInvocationContext context)
        {
            ILog log = DIContainer.DefaultContainer.Resolve<ILog>();
            ITemplateEngine engine = DIContainer.DefaultContainer.Resolve<ITemplateEngine>();
            object c = new
            {
                message = context.Exception.Message,
                time = DateTime.Now,
                methodName = context.Method.Name,
                type = context.ProxyType.FullName
            };
            string text = engine.EvaluateTemplate(ErrorTextFormat, c);
            log.Error(text);
        }
    }
}
