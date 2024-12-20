using Tabu.DTOs.BannedWords;

namespace Tabu.Services.Abstracts;

public interface IBannedWordService
{
    Task<int> Create(BannedWordCreateDto dto);
    Task<IEnumerable<BannedWordGetDto>> GetAllAsync();
    Task<BannedWordGetDto> GetByIdAsync(int id);
    Task UpdateAsync(int id,BannedWordUpdateDto dto);
    Task DeleteAsync(int id);
}