using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Exceptions
{
    public class InvalidUrlStringException : Exception
    {
        public InvalidUrlStringException(string url)
            : base("invalid url for: '{0}'".FormatText(url))
        {

        }
    }
}
