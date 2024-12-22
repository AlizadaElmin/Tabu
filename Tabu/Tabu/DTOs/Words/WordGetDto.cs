namespace Tabu.DTOs.Words;

public class WordGetDto
{
    public string Text { get; set; }
    public string LanguageCode { get; set; } 
    public HashSet<string> BannedWords { get; set; }
}