namespace advance_Csharp.Service.Interface
{
    public interface ILoggingService
    {
        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="exception"></param>
        void LogError(Exception exception);

        /// <summary>
        /// log info
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);
    }
}
