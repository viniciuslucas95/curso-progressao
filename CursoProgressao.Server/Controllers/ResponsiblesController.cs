using CursoProgressao.Server.Data;
using CursoProgressao.Server.Services.Responsibles;
using CursoProgressao.Shared.Dto.Common;
using CursoProgressao.Shared.Dto.Responsibles;
using Microsoft.AspNetCore.Mvc;

namespace CursoProgressao.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResponsiblesController : ControllerBase
{
    private readonly IResponsiblesService _service;
    private readonly SchoolContext _context;

    public ResponsiblesController(IResponsiblesService service, SchoolContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<CreationReturnDto>> PostAsync(CreateResponsibleDto dto)
    {
        Guid id = await _service.CreateAsync(dto);

        await _context.SaveChangesAsync();

        return new CreationReturnDto { Id = id };
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAsync(Guid id, UpdateResponsibleDto dto)
    {
        await _service.UpdateAsync(id, dto);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _service.DeleteAsync(id);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllResponsiblesDto>>> GetAllAsync()
    {
        IEnumerable<GetAllResponsiblesDto> results = await _service.GetAllAsync();

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetOneResponsibleDto>> GetOneAsync(Guid id)
    {
        GetOneResponsibleDto result = await _service.GetOneAsync(id);

        return Ok(result);
    }
}

