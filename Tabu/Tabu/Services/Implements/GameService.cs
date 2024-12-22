using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Tabu.DAL;
using Tabu.DTOs.Games;
using Tabu.Entities;
using Tabu.Exceptions.Game;
using Tabu.Services.Abstracts;

namespace Tabu.Services.Implements;

public class GameService(IMapper _mapper,TabuDBContext _context,IMemoryCache _cache):IGameService
{
    public async Task<Guid> AddAsync(GameCreateDto dto)
    {
        var entity = _mapper.Map<Game>(dto);
        await _context.Games.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Game> GetCurrentStatus(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        if (entity is null) throw new GameNotFoundException();
        if (entity.Score is not null) throw new GameAlreadyFinishedException();
        entity.WrongAnswer = _cache.Get<int>("WrongAnswer");
        entity.FailCount = _cache.Get<int>("FailCount");
        entity.SkipCount = _cache.Get<int>("SkipCount");
        return entity;
    }
    public async Task StartAsync(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        if (entity is null) throw new GameNotFoundException();
        if (entity.Score is not null) throw new GameAlreadyFinishedException();
        _cache.Set("WrongAnswer", entity.WrongAnswer, TimeSpan.FromMinutes(30));
        _cache.Set("FailCount", entity.FailCount, TimeSpan.FromMinutes(30));
        _cache.Set("SkipCount", entity.SkipCount, TimeSpan.FromMinutes(30));
    }
}