using Tabu.DTOs.Words;

namespace Tabu.Services.Abstracts;

public interface IWordService
{
    Task<int> Create(WordCreateDto dto);
    Task<IEnumerable<WordGetDto>> GetAllAsync();
    Task<WordGetDto> GetByIdAsync(int id);
    Task UpdateAsync(int id,WordUpdateDto dto);
    Task DeleteAsync(int id);
}
