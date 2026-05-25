using Microsoft.AspNetCore.Mvc;
using Tut7Solution.DTOs;
using Tut7Solution.Services;

namespace Tut7Solution.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PcResponseDto>>> GetAll()
    {
        var result = await _pcService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}/components")]
    public async Task<ActionResult<PcComponentsResponseDto>> GetComponents(int id)
    {
        var result = await _pcService.GetComponentsAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PcResponseDto>> Create(PcCreateDto dto)
    {
        var result = await _pcService.CreateAsync(dto);
        return Created($"/api/pcs/{result.Id}", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, PcUpdateDto dto)
    {
        var updated = await _pcService.UpdateAsync(id, dto);
        if (!updated)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
