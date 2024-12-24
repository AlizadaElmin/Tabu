using Microsoft.AspNetCore.Mvc;
using Tabu.DTOs.Games;
using Tabu.Exceptions;
using Tabu.ExternalServices.Abstracts;
using Tabu.Services.Abstracts;

namespace Tabu.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IGameService _service,ICacheService _cache):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(GameCreateDto dto)
    {
        return Ok(await _service.AddAsync(dto));
    }

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> Start(Guid id)
    {
        return Ok( await _service.StartAsync(id));
    }

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> Success(Guid id)
    {
        return Ok(await _service.SuccessAsync(id));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameData(Guid id)
    {
        return Ok(await _service.GetCurrentStatus(id));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get(string key)
    {
        return Ok(await _cache.GetAsync<string>(key));
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Set(string key,string value)
    {
        await _cache.SetAsync(key, value,30);
        return Ok();
    }
}
