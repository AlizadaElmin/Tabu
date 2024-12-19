using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabu.DAL;
using Tabu.DTOs.Languages;
using Tabu.Services.Abstracts;

namespace Tabu.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LanguagesController(ILanguageService _service):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLanguages()
    {
        var languages = await  _service.GetAllAsync();
        return Ok(languages);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetLanguageById(string id)
    {
       var language= await _service.GetByCodeAsync(id);
       return Ok(language);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(LanguageCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Created();
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(string id,LanguageUpdateDto dto)
    {
        await _service.UpdateAsync(id,dto);
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
       await _service.DeleteAsync(id);
        return Ok();
    }
}