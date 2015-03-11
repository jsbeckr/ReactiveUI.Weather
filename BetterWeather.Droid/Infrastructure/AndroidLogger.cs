using Android.Util;
using Splat;

namespace Weather.Droid.Infrastructure
{
    public class AndroidLogger : ILogger
    {
        private const string Tag = "WeatherApp";

        public LogLevel Level { get; set; }

        public void Write(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    Log.Info("INFO", message);
                    break;
                case LogLevel.Debug:
                    Log.Debug("DEBUG", message);
                    break;
                case LogLevel.Warn:
                    Log.Warn("WARN", message);
                    break;
                case LogLevel.Error:
                    Log.Error("ERROR", message);
                    break;
                case LogLevel.Fatal:
                    Log.Wtf("FATAL", message);
                    break;
            }
        }
    }
}