using CursoProgressao.Server.Data;
using CursoProgressao.Server.Services.Students;
using CursoProgressao.Shared.Dto.Common;
using CursoProgressao.Shared.Dto.Students;
using Microsoft.AspNetCore.Mvc;

namespace CursoProgressao.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentsService _service;
    private readonly SchoolContext _context;

    public StudentsController(IStudentsService service, SchoolContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<CreationReturnDto>> PostAsync(CreateStudentDto dto)
    {
        Guid id = await _service.CreateAsync(dto);

        await _context.SaveChangesAsync();

        return new CreationReturnDto { Id = id };
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAsync(Guid id, UpdateStudentDto dto)
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
    public async Task<ActionResult<GetAllPartialStudentsDto>> GetAllAsync([FromQuery] GetAllStudentsQueryDto query)
    {
        GetAllPartialStudentsDto result = await _service.GetAllAsync(query);

        return Ok(result);
    }
}
