using System;

namespace Crow.Library.Host.Handler
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResponseModelAttribute : Attribute
    {
        public string ContentType { get; set; }
    }
}
