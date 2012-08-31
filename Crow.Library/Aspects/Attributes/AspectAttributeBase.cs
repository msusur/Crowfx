using System;
using Crow.Library.InjectionContainer;

namespace Crow.Library.Aspects.Attributes
{
    internal enum ExecutionOrder
    {
        Before, After, Exception
    }

    /// <summary>
    /// Base class for Aspect attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AspectAttributeBase : Attribute
    {
        /// <summary>
        /// Gets or sets the execution order of the current attribute 
        /// among all the other attributes used on the same method.
        /// </summary>
        public byte Order { get; set; }

        /// <summary>
        /// Initializes a new instance of AspectAttributeBase.
        /// </summary>
        /// <param name="order">Execution order of the current attribute 
        /// among all the other attributes used on the same method.</param>
        public AspectAttributeBase(byte order)
        {
            Order = order;
        }
        ///// <summary>
        ///// Initializes a new instance of AspectAttributeBase.
        ///// </summary>
        ///// <param name="order">Execution order of the current attribute 
        ///// among all the other attributes used on the same method.</param>
        //internal AspectAttributeBase(IInjectionContainer container, byte order = 0)
        //    : this(order)
        //{
        //    Container = container;
        //}

        /// <summary>
        /// Initializes a new instance of AspectAttributeBase.
        /// </summary>
        public AspectAttributeBase()
            : this(0) { }
    }
}