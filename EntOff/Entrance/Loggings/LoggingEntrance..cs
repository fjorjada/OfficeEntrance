namespace EntOff.Api.Entrance.Loggings
{
    public class LoggingEntrance : ILoggingEntrance
    {
        private readonly ILogger<LoggingEntrance> logger;

        public LoggingEntrance(ILogger<LoggingEntrance> logger) =>
            this.logger = logger;

        public void LogInformation(string message) =>
            this.logger.LogInformation(message);

        public void LogTrace(string message) =>
            this.logger.LogTrace(message);

        public void LogDebug(string message) =>
            this.logger.LogDebug(message);

        public void LogWarning(string message) =>
            this.logger.LogWarning(message);

        public void LogError(Exception exception) =>
            this.logger.LogError(exception.Message, exception);

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);
    }
}
