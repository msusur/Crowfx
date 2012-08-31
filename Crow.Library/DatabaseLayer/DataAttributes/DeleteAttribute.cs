namespace Crow.Library.DatabaseLayer.DataAttributes
{
    /// <summary>
    /// Marker attribute that spesifies the marked method as a Delete operation.
    /// </summary>
    public sealed class DeleteAttribute : DbAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteAttribute"/>.
        /// </summary>
        public DeleteAttribute()
            : base()
        { }
    }
}