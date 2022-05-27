using CursoProgressao.Server.Data;
using CursoProgressao.Server.Services.Payments;
using CursoProgressao.Shared.Dto.Common;
using CursoProgressao.Shared.Dto.Payments;
using Microsoft.AspNetCore.Mvc;

namespace CursoProgressao.Server.Controllers;

[Route("api")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentsService _service;
    private readonly SchoolContext _context;

    public PaymentsController(IPaymentsService service, SchoolContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpPost("contracts/{contractId}/[controller]")]
    public async Task<ActionResult<CreationReturnDto>> PostAsync(Guid contractId, CreatePaymentDto dto)
    {
        Guid id = await _service.CreateAsync(contractId, dto);

        await _context.SaveChangesAsync();

        return new CreationReturnDto { Id = id };
    }

    [HttpPatch("contracts/{contractId}/[controller]/{id}")]
    public async Task<IActionResult> PatchAsync(Guid contractId, Guid id, UpdatePaymentDto dto)
    {
        await _service.UpdateAsync(contractId, id, dto);

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

    [HttpGet("contracts/{contractId}/[controller]")]
    public async Task<ActionResult<IEnumerable<GetAllPaymentsDto>>> GetAllAsync(Guid contractId)
    {
        IEnumerable<GetAllPaymentsDto> results = await _service.GetAllAsync(contractId);

        return Ok(results);
    }
}
