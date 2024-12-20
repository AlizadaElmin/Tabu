namespace Tabu.Exceptions.BannedWord;

public class BannedWordNotFoundException:Exception,IBaseException
{
    public int StatusCode => StatusCodes.Status404NotFound;
    public string ErrorMessage { get; }

    public BannedWordNotFoundException()
    {
        ErrorMessage ="Banned Word not found.";
    }

    public BannedWordNotFoundException(string message) : base(message)
    {
        ErrorMessage=message;
    }
}