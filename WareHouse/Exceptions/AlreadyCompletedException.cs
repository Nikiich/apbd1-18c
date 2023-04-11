namespace WareHouse.Exceptions;

public class AlreadyCompletedException : Exception
{
    public string Message;

    public AlreadyCompletedException(string? message) : base(message)
    {
        Message = message;
    }
}