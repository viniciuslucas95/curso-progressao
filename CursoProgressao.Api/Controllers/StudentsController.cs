using CursoProgressao.Api.Data.UnitOfWork;
using CursoProgressao.Api.Dto.Common;
using CursoProgressao.Api.Dto.Contacts;
using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Dto.Residences;
using CursoProgressao.Api.Dto.Responsibles;
using CursoProgressao.Api.Dto.Students;
using CursoProgressao.Api.Models;
using CursoProgressao.Api.Services.Contacts;
using CursoProgressao.Api.Services.Residences;
using CursoProgressao.Api.Services.ResponsibleDocuments;
using CursoProgressao.Api.Services.Responsibles;
using CursoProgressao.Api.Services.StudentDocuments;
using CursoProgressao.Api.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace CursoProgressao.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentsService _studentsService;
    private readonly IStudentDocumentsService _studentDocumentsService;
    private readonly IResponsiblesService _responsiblesService;
    private readonly IResponsibleDocumentsService _responsibleDocumentsService;
    private readonly IContactsService _contactsService;
    private readonly IResidencesService _residencesService;
    private readonly IUnitOfWork _unitOfWork;

    public StudentsController(IStudentsService studentsService,
        IStudentDocumentsService studentDocumentsService,
        IResponsiblesService responsiblesService,
        IResponsibleDocumentsService responsibleDocumentsService,
        IContactsService contactsService,
        IResidencesService residencesService,
        IUnitOfWork unitOfWork)
    {
        _studentsService = studentsService;
        _studentDocumentsService = studentDocumentsService;
        _responsiblesService = responsiblesService;
        _responsibleDocumentsService = responsibleDocumentsService;
        _contactsService = contactsService;
        _residencesService = residencesService;
        _unitOfWork = unitOfWork;
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchStudentAsync(Guid id, UpdateStudentDto dto)
    {
        Student student = await _studentsService.UpdateAsync(id, dto);

        ModifyStudentChildren(student, dto.Document, dto.Residence, dto.Contact, dto.Responsible);

        await _unitOfWork.CommitAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CreationReturnDto>> PostStudentAsync(CreateStudentDto dto)
    {
        Student student = _studentsService.Create(dto);

        ModifyStudentChildren(student, dto.Document, dto.Residence, dto.Contact, dto.Responsible);

        await _unitOfWork.CommitAsync();

        return new CreationReturnDto { Id = student.Id };
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllStudentsDto>>> GetAllStudentsAsync()
    {
        IEnumerable<GetAllStudentsDto> results = await _studentsService.GetAllAsync();

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetOneStudentDto>> GetOneStudentAsync(Guid id)
    {
        GetOneStudentDto result = await _studentsService.GetOneAsync(id);

        return Ok(result);
    }

    private void ModifyStudentChildren(
        Student student,
        ModifyDocumentDto? documentDto,
        ModifyResidenceDto? residenceDto,
        ModifyContactDto? contactDto,
        ModifyResponsibleDto? responsibleDto)
    {
        if (documentDto is not null)
            _studentDocumentsService.Update(student, documentDto);

        if (residenceDto is not null)
            _residencesService.Update(student, residenceDto);

        if (contactDto is not null)
            _contactsService.Update(student, contactDto);

        if (responsibleDto is not null)
        {
            _responsiblesService.Update(student, responsibleDto);

            if (responsibleDto.Document is not null)
                _responsibleDocumentsService.Update(student, responsibleDto.Document);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        await _studentsService.DeleteAsync(id);

        await _unitOfWork.CommitAsync();

        return NoContent();
    }
}
