using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using log4net.Config;

namespace KTNB.Extended.Commons.Logs
{
    public static class Log
    {
        #region _VARIABLES_

        private const string ConfigFile = "Web.config";
        private static bool _configured;
        private static readonly object SyncLock = new object();

        #endregion

        #region _PROPERTIES_

        /// <summary>
        /// Gets the calling <c>StackFrame</c>.
        /// </summary>
        private static StackFrame CallingFrame
        {
            get
            {
                StackFrame stackFrame = null;
                StackFrame[] stackFrames = new StackTrace().GetFrames();
                int stackLevel = 0;

                if (null != stackFrames)
                {
                    Type reflectedType = stackFrames[stackLevel].GetMethod().ReflectedType;
                    while (reflectedType == typeof(Logger) || reflectedType == typeof(Log))
                    {
                        reflectedType = stackFrames[++stackLevel].GetMethod().ReflectedType;
                    }
                    stackFrame = stackFrames[stackLevel];
                }
                return stackFrame;
            }
        }

        /// <summary>
        /// Gets the calling class.
        /// </summary>
        private static Type CallingType
        {
            get
            {
                return CallingFrame.GetMethod().DeclaringType;
            }
        }

        #endregion

        #region _METHODS_

        /// <summary>
        /// Logs a debugging message.
        /// </summary>
        /// <param name="message">The object represents debugging message.</param>
        public static void Debug(object message)
        {
            Logger logger = GetLogger();
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        /// Logs a formatted debugging message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void DebugFormat(string format, params object[] args)
        {
            Logger logger = GetLogger();
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, args);
            }
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">The object represents the message.</param>
        public static void Info(object message)
        {
            Logger logger = GetLogger();
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// Logs a formatted information message.
        /// </summary>
        /// <param name="message">The object represents the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void InfoFormat(object message, string format, params object[] args)
        {
            InfoFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a formatted information message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Logger logger = GetLogger();
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(provider, format, args);
            }
        }

        /// <summary>
        /// Logs a tracing message.
        /// </summary>
        /// <param name="message">The object represents the message.</param>
        public static void Trace(object message)
        {
            Logger logger = GetLogger();
            if (logger.IsTraceEnabled)
            {
                logger.Trace(message);
            }
        }

        /// <summary>
        /// Logs a formatted tracing message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void TraceFormat(IFormatProvider provider, string format, params object[] args)
        {
            Logger logger = GetLogger();
            if (logger.IsTraceEnabled)
            {
                logger.TraceFormat(provider, format, args);
            }
        }

        /// <summary>
        /// Logs a formatted tracing message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void TraceFormat(string format, params object[] args)
        {
            TraceFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Warn(object message)
        {
            Warn(message, null);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception contains stack trace for the log.</param>
        public static void Warn(object message, Exception exception)
        {
            Logger logger = GetLogger();
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message, exception);
            }
        }

        /// <summary>
        /// Logs a formatted warning message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void WarnFormat(string format, params object[] args)
        {
            WarnFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a formatted warning message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            Logger logger = GetLogger();
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(provider, format, args);
            }
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        //public static void Error(object message)
        //{
        //    Error(message, null);
        //}

        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        public static void Error(Exception exception)
        {
            Error(exception.Message, exception);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception contains strack trace information to the log.</param>
        public static void Error(object message, Exception exception)
        {
            Logger logger = GetLogger();
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, exception);
            }
        }

        /// <summary>
        /// Logs a formatted error message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void ErrorFormat(string format, params object[] args)
        {
            ErrorFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a formatted error message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            Logger logger = GetLogger();
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(provider, format, args);
            }
        }

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Fatal(object message)
        {
            Fatal(message, null);
        }

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception contains strack trace information to the log.</param>
        public static void Fatal(object message, Exception exception)
        {
            Logger logger = GetLogger();
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message, exception);
            }
        }

        /// <summary>
        /// Logs a formatted fatal message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void FatalFormat(string format, params object[] args)
        {
            FatalFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a formatted fatal message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public static void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            Logger logger = GetLogger();
            if (logger.IsFatalEnabled)
            {
                logger.TraceFormat(provider, format, args);
            }
        }


        /// <summary>
        /// Logs a tracing message saying a method has been entered.
        /// </summary>
        public static void MethodEntry()
        {
            Logger logger = GetLogger();
            if (logger.IsTraceEnabled)
            {
                logger.TraceFormat("Entering method [{0}]", CallingFrame.GetMethod().Name);
            }
        }

        /// <summary>
        /// Logs a tracing message saying a method has been exited.
        /// </summary>
        public static void MethodExit()
        {
            Logger logger = GetLogger();
            if (logger.IsTraceEnabled)
            {
                logger.TraceFormat("Method [{0}] returned", CallingFrame.GetMethod().Name);
            }
        }

        /// <summary>
        /// Logs a tracing message saying a method has been exited.
        /// </summary>
        /// <param name="retVal">The returned value from the method.</param>
        public static void MethodExit(object retVal)
        {
            Logger logger = GetLogger();
            if (logger.IsTraceEnabled)
            {
                retVal = null == retVal ? "NULL" : retVal;
                logger.TraceFormat("Method [{0}] returned value [{1}]", CallingFrame.GetMethod().Name, retVal);
            }
        }

        /// <summary>
        /// Ensures configuration file.
        /// </summary>
        private static void EnsureConfig()
        {
            if (!_configured)
            {
                lock (SyncLock)
                {
                    string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFile);
                    if (File.Exists(configPath))
                    {
                        XmlConfigurator.ConfigureAndWatch(new FileInfo(configPath));
                    }
                    _configured = true;
                }
            }
        }

        /// <summary>
        /// Gets current logger.
        /// </summary>
        private static Logger GetLogger()
        {
            EnsureConfig();
            return Logger.GetLogger(CallingType);
        }

        #endregion
    }
}
