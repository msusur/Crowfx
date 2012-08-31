using System;

namespace Crow.Library.Foundation.Exceptions
{
    public class ConversionException : Exception
    {
        public ConversionException()
            : base()
        { }
        public ConversionException(string message)
            : base(message)
        { }
        public ConversionException(string message, Exception inner)
            : base(message, inner)
        { }

    }
}
