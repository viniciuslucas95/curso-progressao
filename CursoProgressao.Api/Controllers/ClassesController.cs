using CursoProgressao.Api.Data.UnitOfWork;
using CursoProgressao.Api.Dto.Classes;
using CursoProgressao.Api.Dto.Common;
using CursoProgressao.Api.Services.Classes;
using Microsoft.AspNetCore.Mvc;

namespace CursoProgressao.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassesController : ControllerBase
{
    private readonly IClassesService _service;
    private readonly SchoolUnitOfWork _unitOfWork;

    public ClassesController(IClassesService classesService, SchoolUnitOfWork unitOfWork)
    {
        _service = classesService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<ActionResult<CreationReturnDto>> PostClassAsync(CreateClassDto dto)
    {
        Guid id = _service.Create(dto);

        await _unitOfWork.CommitAsync();

        return new CreationReturnDto { Id = id };
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchClassAsync(Guid id, UpdateClassDto dto)
    {
        await _service.UpdateAsync(id, dto);

        await _unitOfWork.CommitAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClassAsync(Guid id)
    {
        await _service.DeleteAsync(id);

        await _unitOfWork.CommitAsync();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllClassesDto>>> GetAllClassesAsync()
    {
        IEnumerable<GetAllClassesDto> results = await _service.GetAllAsync();

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetOneClassDto>> GetOneClassAsync(Guid id)
    {
        GetOneClassDto result = await _service.GetOneAsync(id);

        return Ok(result);
    }
}
