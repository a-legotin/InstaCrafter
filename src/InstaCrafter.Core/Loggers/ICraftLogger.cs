namespace InstaCrafter.Core.Loggers
{
    public interface ICraftLogger
    {
        void WriteLog(LogMessageType messageType, string message);
    }
}