namespace Tabu.Exceptions.Game;

public class GameAlreadyFinishedException:Exception,IBaseException
{
    public int StatusCode => StatusCodes.Status404NotFound;
    public string ErrorMessage { get; }

    public GameAlreadyFinishedException()
    {
        ErrorMessage ="Game already finished.";
    }

    public GameAlreadyFinishedException(string message) : base(message)
    {
        ErrorMessage=message;
    }
}