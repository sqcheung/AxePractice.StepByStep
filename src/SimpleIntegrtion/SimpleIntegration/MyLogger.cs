using NLog;

namespace SimpleIntegration
{

    public interface IMyLogger
    {
        void Log(string info);
    }

    public class MyLogger : IMyLogger
    {
        readonly Logger logger;

        public MyLogger(Logger logger)
        {
            this.logger = logger;
        }

        public void Log(string info)
        {
            logger.Log(LogLevel.Info, info);
        }
    }
}