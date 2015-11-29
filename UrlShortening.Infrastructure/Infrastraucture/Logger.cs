using System;
using UrlShortening.Infrastructure.IInfrastructure;
namespace UrlShortening.Infrastructure.Infrastraucture
{
    public class Logger : ILogger
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void LogDebug(object message)
        {
            Log.Debug(message);
        }

        public void LogDebug(object message, Exception exception)
        {
            Log.Debug(message, exception);
        }

        public void LogError(object message)
        {
            Log.Error(message);
        }

        public void LogError(object message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void LogFatal(object message)
        {
            Log.Fatal(message);
        }

        public void LogFatal(object message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public void LogInfo(object message)
        {
            Log.Info(message);
        }

        public void LogInfo(object message, Exception exception)
        {
            Log.Info(message, exception);
        }

        public void LogWarn(object message)
        {
            Log.Warn(message);
        }

        public void LogWarn(object message, Exception exception)
        {
            Log.Warn(message, exception);
        }
    }
}