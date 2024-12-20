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
   
    public async Task<int> Create(BannedWordCreateDto dto)
    {
        var word = _mapper.Map<BannedWord>(dto);
        if (await _context.BannedWords.AnyAsync(w => w.Text == word.Text && w.WordId==dto.WordId))
        {
            throw new BannedWordExistException();
        }
        await _context.BannedWords.AddAsync(word);
        await _context.SaveChangesAsync();
        return word.Id;
    }

    public async Task<IEnumerable<BannedWordGetDto>> GetAllAsync()
    {
        var words = await _context.BannedWords.ToListAsync();
        var wordDtos = _mapper.Map<IEnumerable<BannedWordGetDto>>(words);
        return wordDtos;
    }

    public async Task<BannedWordGetDto> GetByIdAsync(int id)
    {
        var word = await _context.BannedWords.FindAsync(id);
        if (word == null) throw new BannedWordNotFoundException();
        var wordDto = _mapper.Map<BannedWordGetDto>(word);
        return wordDto;
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