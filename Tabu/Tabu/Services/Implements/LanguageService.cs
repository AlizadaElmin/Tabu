using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tabu.DAL;
using Tabu.DTOs.Languages;
using Tabu.Entities;
using Tabu.Services.Abstracts;

namespace Tabu.Services.Implements;

public class LanguageService(TabuDBContext _context,IMapper _mapper):ILanguageService
{
    public async Task<LanguageGetDto> GetByCodeAsync(string code)
    {
        var language = await _context.Languages.FindAsync(code);
        if (language == null) throw new Exception("Language not found");
        var languageDto = _mapper.Map<LanguageGetDto>(language);
        return languageDto;
    }
    public async Task<IEnumerable<LanguageGetDto>> GetAllAsync()
    {
        var languages =  await _context.Languages.ToListAsync();
        var langsDto = _mapper.Map<IEnumerable<LanguageGetDto>>(languages);
        return langsDto;
    }
    public async Task CreateAsync(LanguageCreateDto dto)
    {
        var lang = _mapper.Map<Language>(dto);
        await _context.Languages.AddAsync(lang);
        await _context.SaveChangesAsync();
    }
    

    public async Task UpdateAsync(string code, LanguageUpdateDto dto)
    {
        var language = await _context.Languages.FindAsync(code);
        if(language == null) throw new Exception("Language doesn't exist");
        _mapper.Map(dto, language);
        await  _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string code)
    {
        var language = await _context.Languages.FindAsync(code);
        if(language == null) throw new Exception("Language doesn't exist");
        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();
    }
}