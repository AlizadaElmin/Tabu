namespace Tabu.Exceptions.Word;

public class WordNotFoundException:Exception,IBaseException
{
    public int StatusCode => StatusCodes.Status404NotFound;
    public string ErrorMessage { get; }

    public WordNotFoundException()
    {
        ErrorMessage ="Word not found.";
    }

    public WordNotFoundException(string message) : base(message)
    {
        ErrorMessage=message;
    }
}