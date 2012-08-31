using System;

namespace Crow.Library.Common.ArgControllers
{
    public static class Args
    {
        public static void IsNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
        public static void CheckIf(Func<bool> func, string message)
        {
            if (!func())
            {
                throw new Exception(message);
            }
        }
    }
}