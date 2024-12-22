namespace Tabu.Exceptions.Game;

public class GameNotFoundException:Exception,IBaseException
{
    public int StatusCode => StatusCodes.Status404NotFound;
    public string ErrorMessage { get; }

    public GameNotFoundException()
    {
        ErrorMessage ="Game not found.";
    }

    public GameNotFoundException(string message) : base(message)
    {
        ErrorMessage=message;
    }
}