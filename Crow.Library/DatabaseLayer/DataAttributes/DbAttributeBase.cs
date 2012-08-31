using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.DatabaseLayer.DataAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DbAttributeBase : Attribute
    {
        public DbAttributeBase()
            : base()
        { }
    }
}