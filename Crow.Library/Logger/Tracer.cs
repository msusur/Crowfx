using System.Diagnostics;
using Crow.Library.Common;
using Crow.Library.Foundation.Logger;
using System;
using Castle.Core.Logging;

namespace Crow.Library.Logger
{
    public class DefaultLogger : ILog
    {
        private ILogger logger = new ConsoleLogger("DefaultLogger");

        public void Debug(Func<string> messageFactory)
        {
            logger.Debug(messageFactory);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(string message, System.Exception exception)
        {
            logger.Debug(message, exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            logger.DebugFormat(format, args);
        }

        public void DebugFormat(System.Exception exception, string format, params object[] args)
        {
            logger.DebugFormat(exception, format, args);
        }

        public void DebugFormat(System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.DebugFormat(formatProvider, format, args);
        }

        public void DebugFormat(System.Exception exception, System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.DebugFormat(exception, formatProvider, format, args);
        }

        public void Error(System.Func<string> messageFactory)
        {
            logger.Error(messageFactory);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, System.Exception exception)
        {
            logger.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            logger.ErrorFormat(format, args);
        }

        public void ErrorFormat(System.Exception exception, string format, params object[] args)
        {
            logger.ErrorFormat(exception, format, args);
        }

        public void ErrorFormat(System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.ErrorFormat(formatProvider, format, args);
        }

        public void ErrorFormat(System.Exception exception, System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.ErrorFormat(exception, formatProvider, format, args);
        }

        public void Fatal(System.Func<string> messageFactory)
        {
            logger.Fatal(messageFactory);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(string message, System.Exception exception)
        {
            logger.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            logger.FatalFormat(format, args);
        }

        public void FatalFormat(System.Exception exception, string format, params object[] args)
        {
            logger.FatalFormat(exception, format, args);
        }

        public void FatalFormat(System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.FatalFormat(formatProvider, format, args);
        }

        public void FatalFormat(System.Exception exception, System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.FatalFormat(exception, formatProvider, format, args);
        }

        public void Info(System.Func<string> messageFactory)
        {
            logger.Info(messageFactory);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(string message, System.Exception exception)
        {
            logger.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        public void InfoFormat(System.Exception exception, string format, params object[] args)
        {
            logger.InfoFormat(exception, format, args);
        }

        public void InfoFormat(System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.InfoFormat(formatProvider, format, args);
        }

        public void InfoFormat(System.Exception exception, System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.InfoFormat(exception, formatProvider, format, args);
        }

        public void Warn(System.Func<string> messageFactory)
        {
            logger.Warn(messageFactory);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(string message, System.Exception exception)
        {
            logger.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            logger.WarnFormat(format, args);
        }

        public void WarnFormat(System.Exception exception, string format, params object[] args)
        {
            logger.WarnFormat(exception, format, args);
        }

        public void WarnFormat(System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.WarnFormat(formatProvider, format, args);
        }

        public void WarnFormat(System.Exception exception, System.IFormatProvider formatProvider, string format, params object[] args)
        {
            logger.WarnFormat(exception, formatProvider, format, args);
        }
    }
}
