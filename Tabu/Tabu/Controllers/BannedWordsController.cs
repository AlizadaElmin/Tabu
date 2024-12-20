using Microsoft.AspNetCore.Mvc;
using Tabu.DTOs.BannedWords;
using Tabu.Exceptions;
using Tabu.Services.Abstracts;

namespace Tabu.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BannedWordsController(IBannedWordService _service):ControllerBase
{
     [HttpGet]
    public async Task<IActionResult> GetAllWords()
    {
        return Ok(_service.GetAllAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var word = await _service.GetByIdAsync(id);
            return Ok(word);
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

    [HttpPost]
    public async Task<IActionResult> Post(BannedWordCreateDto dto)
    {
        try
        {
            await _service.Create(dto);
            return Created();
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
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(int id,BannedWordUpdateDto dto)
    {
        try
        {
            await _service.UpdateAsync(id,dto);
            return Ok();
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
    [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
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