namespace Captinslog.Domain;

public class LogEntryException : Exception
{
    public LogEntryException(string message) : base(message)
    {

    }
}