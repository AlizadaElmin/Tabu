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
       var language = await _service.GetByCodeAsync(code);
       return Ok(language);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(LanguageCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Created();
    }
    
    [HttpPut]
    [Route("{code}")]
    public async Task<IActionResult> Put(string code,LanguageUpdateDto dto)
    {
        await _service.UpdateAsync(code,dto);
        return Ok();
    }

    [HttpDelete]
    [Route("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        await _service.DeleteAsync(code);
        return Ok();
    }
}