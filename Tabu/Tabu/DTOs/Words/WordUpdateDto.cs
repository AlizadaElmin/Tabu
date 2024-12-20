namespace Tabu.DTOs.Words;

public class WordUpdateDto
{
    public string Text { get; set; }
    public string LanguageCode { get; set; } 
    public IEnumerable<string> BannedWords { get; set; }
}