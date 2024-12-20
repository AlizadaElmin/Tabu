namespace Tabu.Exceptions.Word;

public class InvalidBannedWordCount:Exception,IBaseException
{

    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public InvalidBannedWordCount()
    {
        ErrorMessage = "Banned word count must be equal to 8";
    }

    public InvalidBannedWordCount(string message):base(message)
    {
        ErrorMessage = message;
    }
}