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
        return Ok(await _service.GetAllAsync());
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