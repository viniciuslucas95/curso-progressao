using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Server.Services.Classes;
using CursoProgressao.Server.Services.Contacts;
using CursoProgressao.Server.Services.Contracts;
using CursoProgressao.Server.Services.Residences;
using CursoProgressao.Server.Services.Responsibles;
using CursoProgressao.Server.Services.StudentDocuments;
using CursoProgressao.Shared.Dto.Contracts;
using CursoProgressao.Shared.Dto.Students;
using CursoProgressao.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CursoProgressao.Server.Services.Students;

public class StudentsService : IStudentsService
{
    private readonly SchoolContext _context;
    private readonly IContractsService _contractsService;
    private readonly IClassesService _classesService;
    private readonly IResponsiblesService _responsiblesService;
    private readonly IContactsService _contactsService;
    private readonly IResidencesService _residencesService;
    private readonly IStudentDocumentsService _studentDocumentsService;
    private readonly NotFoundException _notFoundException = new("StudentNotFound");

    public StudentsService(
        SchoolContext context,
        IContractsService contractsService,
        IClassesService classesService,
        IResponsiblesService responsiblesService,
        IContactsService contactsService,
        IResidencesService residencesService,
        IStudentDocumentsService studentDocumentsService)
    {
        _context = context;
        _contractsService = contractsService;
        _classesService = classesService;
        _responsiblesService = responsiblesService;
        _contactsService = contactsService;
        _residencesService = residencesService;
        _studentDocumentsService = studentDocumentsService;
    }

    public async Task<Guid> CreateAsync(CreateStudentDto dto)
    {
        if (dto.ResponsibleId is not null)
            await _responsiblesService.CheckExistenceAsync((Guid)dto.ResponsibleId);

        Student student = new(dto.FirstName, dto.LastName, dto.BirthDate, dto.Note, dto.ResponsibleId);
        Guid studentId = student.Id;

        while (await DoesExistAsync(studentId))
            student = new(dto.FirstName, dto.LastName, dto.BirthDate, dto.Note, dto.ResponsibleId);

        if (dto.Document is not null)
            await _studentDocumentsService.CreateAsync(studentId, dto.Document);

        if (dto.Contact is not null)
            await _contactsService.CreateAsync(studentId, dto.Contact);

        if (dto.Residence is not null)
            await _residencesService.CreateAsync(studentId, dto.Residence);

        _context.Students.Add(student);

        return studentId;
    }

    public async Task UpdateAsync(Guid id, UpdateStudentDto dto)
    {
        Student student = await GetModelAsync(id);
        Guid studentId = student.Id;

        if (dto.FirstName is not null) student.FirstName = dto.FirstName;
        if (dto.LastName is not null) student.LastName = dto.LastName;
        if (dto.Note is not null) student.Note = dto.Note;
        if (dto.BirthDate is not null) student.BirthDate = dto.BirthDate;

        if (dto.ResponsibleId is not null)
        {
            await _responsiblesService.CheckExistenceAsync((Guid)dto.ResponsibleId);

            student.ResponsibleId = dto.ResponsibleId;
        }

        if (dto.Document is not null)
        {
            if (student.Document is null)
                await _studentDocumentsService.CreateAsync(studentId, dto.Document);
            else
                await _studentDocumentsService.UpdateAsync(studentId, dto.Document);
        }

        if (dto.Contact is not null)
        {
            if (student.Contact is null)
                await _contactsService.CreateAsync(studentId, dto.Contact);
            else
                await _contactsService.UpdateAsync(studentId, dto.Contact);
        }

        if (dto.Residence is not null)
        {
            if (student.Residence is null)
                await _residencesService.CreateAsync(studentId, dto.Residence);
            else
                await _residencesService.UpdateAsync(studentId, dto.Residence);
        }

        if (dto.SetNullList is not null)
            await SetNullsAsync(dto.SetNullList, student);
    }

    public async Task DeleteAsync(Guid id)
    {
        Student student = await GetCompleteModelAsync(id);

        _context.Students.Remove(student);
    }

    public async Task CheckExistenceAsync(Guid id)
    {
        bool doesExist = await _context.Students.AnyAsync(student => student.Id == id);

        if (!doesExist) throw _notFoundException;
    }

    public async Task<GetAllPartialStudentsDto> GetAllAsync(GetAllStudentsQueryDto query)
    {
        IQueryable<Student> studentModels = QueryAllModels(query);
        IQueryable<GetPartialStudentDto> result = studentModels
            .Select(student => new GetPartialStudentDto()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Contact = student.Contact,
                Document = student.Document,
                Responsible = student.Responsible,
                Contract = new ContractsInfoDto()
                {
                    ActiveClassesId = GetActiveClasses(student.Contracts),
                    IsOwing = IsStudentOwing(student.Contracts)
                }
            });

        // BROKEN
        //if (query.IsOwing is not null)
        //    result = result.Where(student => student.Contract.IsOwing == (bool)query.IsOwing);

        // BROKEN
        //if (query.ClassId is not null)
        //{
        //    if (query.ClassId == Guid.Empty)
        //        result = result.Where(student => !student.Contract.ActiveClassesId.Any());
        //    else
        //        result = result.Where(student => student.Contract.ActiveClassesId.Any(id => id == classId));
        //}

        int count = result.Count();

        if (query.Take is not null)
            if (query.Take > 25)
                throw new BadRequestException("TakeLimitIs25");

        int take = query.Take ?? 10;
        int skip = query.Skip ?? 0;

        result = result.Skip(skip).Take(take);

        return new GetAllPartialStudentsDto()
        {
            Count = count,
            Students = await result.ToListAsync()
        };
    }

    private IQueryable<Student> QueryAllModels(GetAllStudentsQueryDto query)
    {
        IQueryable<Student> students = _context.Students.AsNoTracking();

        if (query.FirstName is not null)
            students = students.Where(student => student.FirstName == query.FirstName);

        if (query.LastName is not null)
            students = students.Where(student => student.LastName == query.LastName);

        if (query.Rg is not null)
            students = students.Where(student => student.Document != null && student.Document.Rg == query.Rg);

        if (query.Cpf is not null)
            students = students.Where(student => student.Document != null && student.Document.Cpf == query.Cpf);

        if (query.ResponsibleFirstName is not null)
            students = students.Where(student => student.Responsible != null && student.Responsible.FirstName == query.ResponsibleFirstName);

        if (query.ResponsibleLastName is not null)
            students = students.Where(student => student.Responsible != null && student.Responsible.LastName == query.ResponsibleLastName);

        if (query.ResponsibleRg is not null)
            students = students.Where(student => student.Responsible != null && student.Responsible.Document.Rg == query.Rg);

        if (query.ResponsibleCpf is not null)
            students = students.Where(student => student.Responsible != null && student.Responsible.Document.Cpf == query.Cpf);

        return students
            .Include("Responsible.Document")
            .Include("Contracts")
            .Include("Contracts.Payments");
    }

    private async Task<Student> GetModelAsync(Guid id)
    {
        Student? student = await _context.Students
            .Where(student => student.Id == id)
            .Include("Document")
            .Include("Responsible")
            .Include("Contact")
            .Include("Residence")
            .Include("Responsible.Document")
            .FirstOrDefaultAsync();

        if (student is null) throw _notFoundException;

        return student;
    }

    private async Task<Student> GetCompleteModelAsync(Guid id)
    {
        Student? student = await _context.Students
            .Where(student => student.Id == id)
            .Include("Document")
            .Include("Responsible")
            .Include("Contact")
            .Include("Residence")
            .Include("Responsible.Document")
            .Include("Student.Contracts")
            .Include("Student.Contracts.Payments")
            .FirstOrDefaultAsync();

        if (student is null) throw _notFoundException;

        return student;
    }

    private async Task<bool> DoesExistAsync(Guid id)
        => await _context.Students.AnyAsync(student => student.Id == id);

    private async Task SetNullsAsync(string[] setNullList, Student student)
    {
        List<string> remainingProps = new();
        Guid studentId = student.Id;
        BadRequestException studentWithoutInfoException = new(
                            "StudentWithoutInfo",
                            "Student cannot be without cpf and responsible at the same time");

        foreach (string propToSetNull in setNullList)
        {
            string prop = propToSetNull.ToPascalCase();

            switch (prop)
            {
                case "Note":
                    student.Note = null;
                    continue;
                case "BirthDate":
                    student.BirthDate = null;
                    continue;
                case "ResponsibleId":
                    if (student.Document is null)
                        throw studentWithoutInfoException;

                    student.ResponsibleId = null;
                    continue;
                case "Document":
                    if (student.Document is null) continue;
                    if (student.ResponsibleId is null)
                        throw studentWithoutInfoException;

                    await _studentDocumentsService.DeleteAsync(studentId);
                    continue;
                case "Contact":
                    if (student.Contact is null) continue;

                    await _contactsService.DeleteAsync(studentId);
                    continue;
                case "Residence":
                    if (student.Residence is null) continue;

                    await _residencesService.DeleteAsync(studentId);
                    continue;
                case "Rg":
                    if (student.Document is null) continue;

                    student.Document.Rg = null;
                    continue;
                case "Cpf":
                    if (student.Document is null) continue;
                    if (student.ResponsibleId is null)
                        throw studentWithoutInfoException;

                    student.Document.Cpf = null;
                    continue;
                case "Email":
                    if (student.Contact is null) continue;

                    student.Contact.Email = null;
                    continue;
                case "Landline":
                    if (student.Contact is null) continue;

                    student.Contact.Landline = null;
                    continue;
                case "CellPhone":
                    if (student.Contact is null) continue;

                    student.Contact.CellPhone = null;
                    continue;
                case "ZipCode":
                    if (student.Residence is null) continue;

                    student.Residence.ZipCode = null;
                    continue;
                case "Address":
                    if (student.Residence is null) continue;

                    student.Residence.Address = null;
                    continue;
                default:
                    remainingProps.Add(prop);
                    continue;
            }
        }

        if (remainingProps.Count > 0)
            throw new BadRequestException(
                "InvalidProp",
                $"{GetSentenceWithPropsName(remainingProps)} doesn't exist on the model or it cannot be set to null");
    }

    private static StringBuilder GetSentenceWithPropsName(List<string> remainingProps)
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append(remainingProps[0]);

        for (int i = 1; i < remainingProps.Count; i++)
        {
            if (i == remainingProps.Count - 1) stringBuilder.Append(" and ");
            else stringBuilder.Append(", ");

            stringBuilder.Append($"{remainingProps[i]}");
        }

        return stringBuilder;
    }

    private static bool IsStudentOwing(IReadOnlyCollection<Contract> contracts)
        => contracts.Any(contract => ContractsService.IsOwing(contract));

    private static IEnumerable<Guid> GetActiveClasses(IReadOnlyCollection<Contract> contracts)
        => contracts
            .Where(contract => ContractsService.IsActive(contract))
            .Select(contract => contract.ClassId);
}
