using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabu.DAL;
using Tabu.DTOs.Languages;
using Tabu.Exceptions;
using Tabu.Services.Abstracts;

namespace Tabu.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LanguagesController(ILanguageService _service):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLanguages()
    {
        return Ok(await  _service.GetAllAsync());
    }

    [HttpGet]
    [Route("{code}")]
    public async Task<IActionResult> GetLanguageById(string code)
    {
        try
        {
            var language = await _service.GetByCodeAsync(code);
            return Ok(language);
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
    public async Task<IActionResult> Post(LanguageCreateDto dto)
    {
        try
        {
            await _service.CreateAsync(dto);
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
    [Route("{code}")]
    public async Task<IActionResult> Put(string code,LanguageUpdateDto dto)
    {
        try
        {
            await _service.UpdateAsync(code,dto);
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
    [Route("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        try
        {
            await _service.DeleteAsync(code);
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