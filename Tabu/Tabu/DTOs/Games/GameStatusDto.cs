using Tabu.DTOs.Words;

namespace Tabu.DTOs.Games;

public class GameStatusDto
{
    public byte Wrong { get; set; }
    public byte Success { get; set; }
    public byte Pass { get; set; }
    public byte MaxPassCount { get; set; }
    public string LangCode { get; set; }
    public Stack<WordForGameDto>  Words { get; set; }
    public List<int> UsedWordsIds { get; set; }
}