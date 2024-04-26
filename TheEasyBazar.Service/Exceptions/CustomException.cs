namespace TheEasyBazar.Service.Exceptions;

public class CustomException : Exception
{
    public int StatusCode { get; set; }

    public CustomException(int Code, string Message) : base(Message)
    {
        StatusCode = Code;
    }
}
