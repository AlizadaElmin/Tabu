using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tabu.DAL;
using Tabu.DTOs.BannedWords;
using Tabu.Entities;
using Tabu.Exceptions.BannedWord;
using Tabu.Services.Abstracts;

namespace Tabu.Services.Implements;

public class BannedWordService(TabuDBContext _context,IMapper _mapper):IBannedWordService
{
    public async Task<IEnumerable<BannedWordGetDto>> GetAllAsync()
    {
        var words = await _context.BannedWords.ToListAsync();
        var wordDtos = _mapper.Map<IEnumerable<BannedWordGetDto>>(words);
        return wordDtos;
    }
    
    public async Task UpdateAsync(int id, BannedWordUpdateDto dto)
    {
        var word = await _context.BannedWords.FindAsync(id);
        if (word == null) throw new BannedWordNotFoundException();
        _mapper.Map(dto, word);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var word = await _context.BannedWords.FindAsync(id);
        if (word == null) throw new  BannedWordNotFoundException();
        _context.BannedWords.Remove(word);
        await _context.SaveChangesAsync();
    }
}