using CursoProgressao.Server.Data;
using CursoProgressao.Server.Dto.Classes;
using CursoProgressao.Server.Dto.Common;
using CursoProgressao.Server.Services.Classes;
using Microsoft.AspNetCore.Mvc;

namespace CursoProgressao.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassesController : ControllerBase
{
    private readonly IClassesService _service;
    private readonly SchoolContext _context;

    public ClassesController(IClassesService service, SchoolContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<CreationReturnDto>> PostAsync(CreateClassDto dto)
    {
        Guid id = await _service.CreateAsync(dto);

        await _context.SaveChangesAsync();

        return new CreationReturnDto { Id = id };
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAsync(Guid id, UpdateClassDto dto)
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
    public async Task<ActionResult<IEnumerable<GetAllClassesDto>>> GetAllAsync()
    {
        IEnumerable<GetAllClassesDto> results = await _service.GetAllAsync();

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetOneClassDto>> GetOneAsync(Guid id)
    {
        GetOneClassDto result = await _service.GetOneAsync(id);

        return Ok(result);
    }
}
