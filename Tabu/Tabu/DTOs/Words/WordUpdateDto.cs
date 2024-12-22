namespace Tabu.DTOs.Words;

public class WordUpdateDto
{
    public string Text { get; set; }
    public string LanguageCode { get; set; } 
    public HashSet<string> BannedWords { get; set; }
}