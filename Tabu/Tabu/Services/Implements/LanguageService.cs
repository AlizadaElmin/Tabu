using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tabu.DAL;
using Tabu.DTOs.Languages;
using Tabu.Entities;
using Tabu.Exceptions.Language;
using Tabu.Services.Abstracts;
using Tabu.Validators.Language;

namespace Tabu.Services.Implements;

public class LanguageService(TabuDBContext _context, IMapper _mapper) : ILanguageService
{
    public async Task<LanguageGetDto> GetByCodeAsync(string code)
    {
        var language = await _context.Languages.FindAsync(code);
        if (language == null) throw new LanguageNotFoundException();
        var languageDto = _mapper.Map<LanguageGetDto>(language);
        return languageDto;
    }

    public async Task<IEnumerable<LanguageGetDto>> GetAllAsync()
    {
        var languages = await _context.Languages.ToListAsync();
        var langsDto = _mapper.Map<IEnumerable<LanguageGetDto>>(languages);
        return langsDto;
    }

    public async Task CreateAsync(LanguageCreateDto dto)
    {
        var lang = _mapper.Map<Language>(dto);
        if (await _context.Languages.AnyAsync(x => x.Code == dto.Code))
        {
            throw new LanguageExistException();
        }

        await _context.Languages.AddAsync(lang);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(string code, LanguageUpdateDto dto)
    {
        var language = await _getByCode(code);
        if (language == null) throw new LanguageNotFoundException();
        _mapper.Map(dto, language);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string code)
    {
        var language = await _context.Languages.FindAsync(code);
        if (language == null) throw new  LanguageNotFoundException();
        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();
    }

    async Task<Language?> _getByCode(string code)
        => await _context.Languages.FindAsync(code);
}