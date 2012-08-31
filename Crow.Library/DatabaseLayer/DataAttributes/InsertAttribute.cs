using System;

namespace Crow.Library.DatabaseLayer.DataAttributes
{
    /// <summary>
    /// Marker attribute that spesifies the marked method as an Insert operation.
    /// </summary>
    public sealed class InsertAttribute : DbAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InsertAttribute"/>.
        /// </summary>
        public InsertAttribute()
            : base()
        { }
    }
}