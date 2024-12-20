using Tabu.Exceptions;

namespace Tabu.Exceptions.Language;

public class LanguageNotFoundException:Exception,IBaseException
{
    public int StatusCode => StatusCodes.Status404NotFound;
    public string ErrorMessage { get; }

    public LanguageNotFoundException()
    {
        ErrorMessage ="Language not found.";
    }

    public LanguageNotFoundException(string message) : base(message)
    {
        ErrorMessage=message;
    }
}