namespace Tabu.DTOs.Words;

public class WordForGameDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<string> BannedWords { get; set; }
}