namespace Tabu.Exceptions.BannedWord;

public class BannedWordExistException:Exception,IBaseException
{
    public int StatusCode => StatusCodes.Status409Conflict;
   
    public string ErrorMessage { get; }

    public BannedWordExistException()
    {
        ErrorMessage = "Banned Word already added database.";
    }

    public BannedWordExistException(string message):base(message)
    {
        ErrorMessage = message;
    }
}