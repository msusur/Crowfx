using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Aspects.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class WorksBeforeAttribute : Attribute
    {
    }
}
