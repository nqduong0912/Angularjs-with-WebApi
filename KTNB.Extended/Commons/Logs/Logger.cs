using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using log4net;
using log4net.Core;
using log4net.Util;

namespace KTNB.Extended.Commons.Logs
{
    public class Logger : LoggerWrapperImpl
    {
        #region _VARIABLES_

        private readonly Type _stackBoundary = typeof(Logger);

        #endregion

        #region _PROPERTIES_

        /// <summary>
        /// Indicates whether or not the current logger is enabled for DEBUG level.
        /// </summary>
        public bool IsDebugEnabled
        {
            get
            {
                return this.Logger.IsEnabledFor(Level.Debug);
            }
        }

        /// <summary>
        /// Indicates whether or not the current logger is enabled for TRACE level.
        /// </summary>
        public bool IsTraceEnabled
        {
            get
            {
                return this.Logger.IsEnabledFor(Level.Trace);
            }
        }

        /// <summary>
        /// Indicates whether or not the current logger is enabled for INFO level.
        /// </summary>
        public bool IsInfoEnabled
        {
            get
            {
                return this.Logger.IsEnabledFor(Level.Info);
            }
        }

        /// <summary>
        /// Indicates whether or not the current logger is enabled for WARN level.
        /// </summary>
        public bool IsWarnEnabled
        {
            get
            {
                return this.Logger.IsEnabledFor(Level.Warn);
            }
        }

        /// <summary>
        /// Indicates whether or not the current logger is enabled for ERROR level.
        /// </summary>
        public bool IsErrorEnabled
        {
            get
            {
                return this.Logger.IsEnabledFor(Level.Error);
            }
        }

        /// <summary>
        /// Indicates whether or not the current logger is enabled for FATAL level.
        /// </summary>
        public bool IsFatalEnabled
        {
            get
            {
                return this.Logger.IsEnabledFor(Level.Fatal);
            }
        }

        #endregion

        #region _METHODS_

        /// <summary>
        /// Initalises a new instance of <c>Logger</c>.
        /// </summary>
        /// <param name="logger"></param>
        public Logger(ILogger logger)
            : base(logger)
        {
            StackFrame[] stackFrames = new StackTrace().GetFrames();
            if (null != stackFrames)
            {
                int stackLevel = 0;
                Type reflectedType = stackFrames[stackLevel].GetMethod().ReflectedType;
                while (reflectedType == typeof(Logger) || reflectedType == typeof(Log))
                {
                    stackLevel++;
                    reflectedType = stackFrames[stackLevel].GetMethod().ReflectedType;
                }
                _stackBoundary = stackFrames[stackLevel - 1].GetMethod().DeclaringType;
            }
            else
            {
                _stackBoundary = typeof(Logger);
            }
        }

        /// <summary>
        /// Gets logger associated with a type.
        /// </summary>
        /// <param name="type">Type to get logger.</param>
        public static Logger GetLogger(Type type)
        {
            return new Logger(LogManager.GetLogger(Assembly.GetCallingAssembly(), type).Logger);
        }

        /// <summary>
        /// Gets logger by specified name.
        /// </summary>
        /// <param name="name">Name of the logger.</param>
        /// <returns></returns>
        public static Logger GetLogger(string name)
        {
            return new Logger(LogManager.GetLogger(name).Logger);
        }

        /// <summary>
        /// Gets a specified context property value.
        /// </summary>
        /// <param name="key">The key associated with value.</param>
        /// <returns>Property value.</returns>
        public static string GetContextProperty(string key)
        {
            return ThreadContext.Properties[key].ToString();
        }

        /// <summary>
        /// Sets a context property value.
        /// </summary>
        /// <param name="key">The key associated with value.</param>
        /// <param name="value">The value to set to the property.</param>
        public static void SetContextProperty(string key, string value)
        {
            ThreadContext.Properties[key] = value;
        }

        /// <summary>
        /// Logs a debugging message.
        /// </summary>
        /// <param name="message">The object represents debugging message.</param>
        public void Debug(object message)
        {
            Log(Level.Debug, message);
        }

        /// <summary>
        /// Logs a formatted debugging message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void DebugFormat(string format, params object[] args)
        {
            Log(Level.Debug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args));
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">The object represents the message.</param>
        public void Info(object message)
        {
            Log(Level.Info, message);
        }

        /// <summary>
        /// Logs a formatted information message.
        /// </summary>
        /// <param name="message">The object represents the message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void InfoFormat(object message, string format, params object[] args)
        {
            InfoFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a formatted information message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log(Level.Info, new SystemStringFormat(provider, format, args));
        }

        /// <summary>
        /// Logs a tracing message.
        /// </summary>
        /// <param name="message">The object represents the message.</param>
        public void Trace(object message)
        {
            Log(Level.Trace, message);
        }

        /// <summary>
        /// Logs a formatted tracing message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void TraceFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log(Level.Trace, new SystemStringFormat(provider, format, args));
        }

        /// <summary>
        /// Logs a formatted tracing message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void TraceFormat(string format, params object[] args)
        {
            TraceFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Warn(object message)
        {
            Log(Level.Warn, message);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception contains stack trace for the log.</param>
        public void Warn(object message, Exception exception)
        {
            Log(Level.Warn, message, exception);
        }

        /// <summary>
        /// Logs a formatted warning message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void WarnFormat(string format, params object[] args)
        {
            WarnFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a formatted warning message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log(Level.Warn, new SystemStringFormat(provider, format, args));
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Error(object message)
        {
            Error(message, null);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception contains strack trace information to the log.</param>
        public void Error(object message, Exception exception)
        {
            Log(Level.Error, message, exception);
        }

        /// <summary>
        /// Logs a formatted error message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void ErrorFormat(string format, params object[] args)
        {
            ErrorFormat(null, format, args);
        }

        /// <summary>
        /// Logs a formatted error message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log(Level.Error, new SystemStringFormat(provider, format, args));
        }

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Fatal(object message)
        {
            Fatal(message, null);
        }

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception contains strack trace information to the log.</param>
        public void Fatal(object message, Exception exception)
        {
            Log(Level.Fatal, message, exception);
        }

        /// <summary>
        /// Logs a formatted fatal message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void FatalFormat(string format, params object[] args)
        {
            FatalFormat(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Logs a formatted fatal message.
        /// </summary>
        /// <param name="provider">The provider that provides mechanism for retrieving an object to control formatting.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log(Level.Fatal, new SystemStringFormat(provider, format, args));
        }

        /// <summary>
        /// Logs a message with specified level.
        /// </summary>
        /// <param name="level">Level of the log.</param>
        /// <param name="message">The message.</param>
        private void Log(Level level, object message)
        {
            Log(level, message, null);
        }

        /// <summary>
        /// Logs a message with specified level.
        /// </summary>
        /// <param name="level">Level of the log.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        private void Log(Level level, object message, Exception exception)
        {
            Logger.Log(_stackBoundary, level, message, exception);
        }

        #endregion
    }
}
