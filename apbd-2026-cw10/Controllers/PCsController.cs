using apbd_2026_cw10.Data;
using apbd_2026_cw10.DTOs;
using apbd_2026_cw10.Entities;
using apbd_2026_cw10.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_cw10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PCsController : ControllerBase
{
    private readonly IDbService _dbService;
    public PCsController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var PCs = await _dbService.GetAllPCsAsync();
        return Ok(PCs);
    }

    [Route("{id}/components")]
    [HttpGet]
    public async Task<IActionResult> GetPcWithComponentsById(int id)
    {
        try
        {
            var result = await _dbService.GetPCWithComponentsById(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<PCResponseDto>> CreatePCAsync([FromBody] CreatePCDto createPcDto)
    {
        if (createPcDto == null)
        {
            return BadRequest();
        }
        
        await _dbService.CreatePCAsync(createPcDto);
        return Created();
    }

    [Route("{id}")]
    [HttpPut]
    public async Task<IActionResult> UpdatePCAsync(int id, [FromBody] UpdatePCDto updatePcDto)
    {
        try
        {
            await _dbService.UpdatePCAsync(id, updatePcDto);
            return NoContent();
        } catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeletePCAsync(int id)
    {
        try
        {
            await _dbService.DeletePCAsync(id);
            return NoContent();
        } catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
}