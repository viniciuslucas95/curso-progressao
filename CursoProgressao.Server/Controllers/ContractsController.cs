using CursoProgressao.Server.Data;
using CursoProgressao.Server.Services.Contracts;
using CursoProgressao.Server.Services.Students;
using CursoProgressao.Shared.Dto.Common;
using CursoProgressao.Shared.Dto.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CursoProgressao.Server.Controllers;

[Route("api")]
[ApiController]
public class ContractsController : ControllerBase
{
    private readonly IContractsService _service;
    private readonly SchoolContext _context;

    public ContractsController(IContractsService service, SchoolContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpPost]
    [Route("students/{studentId}/[controller]")]
    public async Task<ActionResult<CreationReturnDto>> PostAsync(
        Guid studentId,
        CreateContractDto dto,
        [FromServices] IStudentsService studentsService)
    {
        Guid id = await _service.CreateAsync(studentId, dto, studentsService.CheckExistenceAsync);

        await _context.SaveChangesAsync();

        return new CreationReturnDto { Id = id };
    }

    [HttpPatch]
    [Route("[controller]/{id}")]
    public async Task<IActionResult> PatchAsync(Guid id, UpdateContractDto dto)
    {
        await _service.UpdateAsync(id, dto);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("[controller]/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _service.DeleteAsync(id);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet]
    [Route("students/{studentId}/[controller]")]
    public async Task<ActionResult<IEnumerable<GetAllContractsDto>>> GetAllAsync(Guid studentId)
    {
        IEnumerable<GetAllContractsDto> results = await _service.GetAllAsync(studentId);

        return Ok(results);
    }
}
