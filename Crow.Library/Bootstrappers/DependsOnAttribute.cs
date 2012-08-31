using System;
using Crow.Library.Foundation.Bootstrapper;


namespace Crow.Library.Bootstrappers
{
    /// <summary>
    /// Spesifies that the given startup installer is DependsOn the given installer.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type that depends on.
        /// </summary>
        public Type DependencyType { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DependsOnAttribute"/>.
        /// </summary>
        public DependsOnAttribute()
        {

        }
        /// <summary>
        /// Initializes a new instance of <see cref="DependsOnAttribute"/>.
        /// </summary>
        public DependsOnAttribute(string type)
            : this(Type.GetType(type))
        {

        }
        /// <summary>
        /// Initializes a new instance of <see cref="DependsOnAttribute"/>.
        /// </summary>
        public DependsOnAttribute(Type dependencyType)
        {
            dependencyType.ThrowIfNull("dependencyType");
            if (!typeof(IModule).IsAssignableFrom(dependencyType))
            {
                throw new ArgumentException("DependsOn attribute must be depends on a class that implements the IStartupInstaller.");
            }
            DependencyType = dependencyType;
        }
    }
}
