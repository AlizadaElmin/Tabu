using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Tabu.DAL;
using Tabu.DTOs.Games;
using Tabu.DTOs.Words;
using Tabu.Entities;
using Tabu.Exceptions.Game;
using Tabu.ExternalServices.Abstracts;
using Tabu.Services.Abstracts;

namespace Tabu.Services.Implements;

public class GameService(IMapper _mapper,TabuDBContext _context,IMemoryCache _cache,ICacheService _cacheService):IGameService
{
    private IGameService _gameServiceImplementation;

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
        #region cache
        entity.WrongAnswer = _cache.Get<int>("WrongAnswer");
        entity.FailCount = _cache.Get<int>("FailCount");
        entity.SkipCount = _cache.Get<int>("SkipCount");
        #endregion
        return entity;
    }

    public async Task<WordForGameDto>  PassAsync(Guid id)
    {
        var status = await _getGameStatusAsync(id);
        await _addNewWords(status);
        if (status.Pass < status.MaxPassCount)
        {
            status.Pass++;
            var word = status.Words.Pop();
            await _cacheService.SetAsync(id.ToString(),status);
            return word;
        }
        return null;
    }

    public async Task<WordForGameDto>  SuccessAsync(Guid id)
    {
        var status = await _getGameStatusAsync(id);
        await _addNewWords(status);
        status.Success++;
        var word = status.Words.Pop();
        await _cacheService.SetAsync(id.ToString(), status);
        return word;
    }

    public async Task<WordForGameDto>  WrongAsync(Guid id)
    {
        var status = await _getGameStatusAsync(id);
        await _addNewWords(status);
        status.Wrong++;
        var word = status.Words.Pop();
        await _cacheService.SetAsync(id.ToString(), status);
        return word;
    }

    public async Task EndAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<WordForGameDto>  StartAsync(Guid id)
    {
        var entity = await _context.Games.FindAsync(id);
        if (entity is null) throw new GameNotFoundException();
        #region cache
        // if (entity.Score is not null) throw new GameAlreadyFinishedException();
        // _cache.Set("WrongAnswer", entity.WrongAnswer, TimeSpan.FromMinutes(30));
        // _cache.Set("FailCount", entity.FailCount, TimeSpan.FromMinutes(30));
        // _cache.Set("SkipCount", entity.SkipCount, TimeSpan.FromMinutes(30));
        #endregion

        var words = await _context.Words.Where(x => x.LanguageCode == entity.LanguageCode).Take(10).Select(x=> new WordForGameDto
        {
            Id = x.Id,
            Text = x.Text,
            BannedWords = x.BannedWords.Select(y=>y.Text).ToList()
        }).ToListAsync();

        GameStatusDto status = new GameStatusDto
        {
            Pass = 0,
            Success = 0,
            Words = new Stack<WordForGameDto>(words),
            UsedWordsIds = words.Select(x => x.Id).ToList(),
            LangCode = entity.LanguageCode,
            MaxPassCount = (byte)entity.SkipCount
        };
        var word = status.Words.Pop();
        await _cacheService.SetAsync(id.ToString(), status);
        return word;
    }

    async Task<GameStatusDto> _getGameStatusAsync(Guid id)
    {
        GameStatusDto status = await _cacheService.GetAsync<GameStatusDto>(id.ToString());
        if (status is null) 
            throw new GameNotFoundException();
        return status;
    }

    async Task _addNewWords(GameStatusDto status)
    {
        if (status.Words.Count < 6)
        {
            var newWords = await _context.Words.Where(w=>w.LanguageCode == status.LangCode && !status.UsedWordsIds.Contains(w.Id)).Take(5).Select(x=> new WordForGameDto
            {
                Id = x.Id,
                Text = x.Text,
                BannedWords = x.BannedWords.Select(y=>y.Text).ToList()
            }).ToListAsync();
            status.UsedWordsIds.AddRange(newWords.Select(x => x.Id));
            newWords.ForEach(x=>status.Words.Push(x));
        }
    }
}