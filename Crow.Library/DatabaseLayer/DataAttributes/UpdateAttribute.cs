namespace Crow.Library.DatabaseLayer.DataAttributes
{
    /// <summary>
    /// Marker attribute that spesifies the marked method as an Update operation.
    /// </summary>
    public sealed class UpdateAttribute : DbAttributeBase
    {
        /// <summary>
        /// Initializes the <see cref="UpdateAttribute"/>.
        /// </summary>
        public UpdateAttribute()
            : base()
        { }
    }
}
