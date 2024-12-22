using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tabu.DAL;
using Tabu.DTOs.Languages;
using Tabu.DTOs.Words;
using Tabu.Entities;
using Tabu.Exceptions.Word;
using Tabu.Services.Abstracts;

namespace Tabu.Services.Implements;

public class WordService(TabuDBContext _context,IMapper _mapper):IWordService
{
    public async Task<int> Create(WordCreateDto dto)
    {
        var word = _mapper.Map<Word>(dto);
        if (await _context.Words.AnyAsync(w => w.LanguageCode == dto.LanguageCode && w.Text == dto.Text))
        {
            throw new WordExistException();
        }

        if (dto.BannedWords.Count() != 8)
        {
            throw new InvalidBannedWordCount();
        }
        // Word word = new Word
        // {
        //     LanguageCode = dto.LanguageCode,
        //     Text = dto.Text,
        //     BannedWords = dto.BannedWords.Select(x => new BannedWord
        //     {
        //         Text = x,
        //
        //     }).ToList()
        // };
        
        word.BannedWords = dto.BannedWords.Select(x=>new BannedWord
        {
            Text = x
        }).ToList();;
        
        
        await _context.Words.AddAsync(word);
        await _context.SaveChangesAsync();
        return word.Id;
    }

    public async Task<IEnumerable<WordGetDto>> GetAllAsync()
    {
        var words = await _context.Words.Include(w=>w.BannedWords).ToListAsync();
        var wordDtos = _mapper.Map<IEnumerable<WordGetDto>>(words);
        return wordDtos;
    }

    public async Task<WordGetDto> GetByIdAsync(int id)
    {
        var word = await _context.Words.Include(w=>w.BannedWords).FirstOrDefaultAsync(w => w.Id == id);
        if (word == null) throw new WordNotFoundException();
        var wordDto = _mapper.Map<WordGetDto>(word);
        return wordDto;
    }

    public async Task UpdateAsync(int id,WordUpdateDto dto)
    {
        var word = await _context.Words.FindAsync(id);
        if (word == null) throw new WordNotFoundException();
        _mapper.Map(dto, word);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var word = await _context.Words.FindAsync(id);
        if (word == null) throw new  WordNotFoundException();
        _context.Words.Remove(word);
        await _context.SaveChangesAsync();
    }
}