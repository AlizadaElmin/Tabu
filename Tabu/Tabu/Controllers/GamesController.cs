using Microsoft.AspNetCore.Mvc;
using Tabu.DTOs.Games;
using Tabu.Exceptions;
using Tabu.Services.Abstracts;

namespace Tabu.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IGameService _service):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(GameCreateDto dto)
    {
        return Ok(await _service.AddAsync(dto));
    }

    [HttpPost("Start/{id}")]
    public async Task<IActionResult> Start(Guid id)
    {
        try
        {
            await _service.StartAsync(id);
            return Ok("Game started");
        }
        catch (Exception ex)
        {
            if (ex is IBaseException ibe)
            {
                return StatusCode(ibe.StatusCode, new
                {
                    StatusCode = ibe.StatusCode,
                    Message = ibe.ErrorMessage,
                });
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                });
            }
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGameData(Guid id)
    {
        try
        {
            return Ok(await _service.GetCurrentStatus(id));
        }
        catch (Exception ex)
        {
            if (ex is IBaseException ibe)
            {
                return StatusCode(ibe.StatusCode, new
                {
                    StatusCode = ibe.StatusCode,
                    Message = ibe.ErrorMessage,
                });
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                });
            }
        }
    }
 
}
