using Crow.Library.DatabaseLayer.DataAttributes;
using System;

namespace Crow.Library.Interceptors.DatabaseInceptors.DbOperations
{
    /// <summary>
    /// Marker attribute for the Repository Factory that says the method is going to be execute a scalar query.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ScalarAttribute : DbAttributeBase
    {
        /// <summary>
        /// Initializes the <see cref="ScalarAttribute"/>.
        /// </summary>
        public ScalarAttribute()
            : base()
        { }
    }
}
