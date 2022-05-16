using CursoProgressao.Server.Data;
using CursoProgressao.Server.Dto.Contacts;
using CursoProgressao.Server.Dto.Documents;
using CursoProgressao.Server.Dto.Residences;
using CursoProgressao.Server.Dto.Students;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Students;

public class StudentsService : IStudentsService
{
    private readonly SchoolContext _context;

    public StudentsService(SchoolContext context) => _context = context;

    public async Task<Guid> CreateAsync(CreateStudentDto dto)
    {
        if (dto.ResponsibleId is not null)
            if (!await CheckResponsibleExistenceAsync((Guid)dto.ResponsibleId)) throw new BadRequestException("ResponsibleNotFound");

        Student student = new(dto.FirstName, dto.LastName, dto.Note, dto.ResponsibleId);

        while (!await CheckStudentIdUniquenessAsync(student.Id)) student = new(dto.FirstName, dto.LastName, dto.Note, dto.ResponsibleId);

        UpdateDocumentDto? document = dto.Document is not null ? new()
        {
            Rg = dto.Document.Rg,
            Cpf = dto.Document.Cpf
        } : null;

        await SetOptionalDataAsync(_context, student, document, dto.Contact, dto.Residence);

        _context.Students.Add(student);

        return student.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateStudentDto dto)
    {
        Student student = await GetOneModelAsync(id);

        if (dto.FirstName is not null) student.FirstName = dto.FirstName;
        if (dto.LastName is not null) student.LastName = dto.LastName;
        if (dto.IsActive is not null) student.IsActive = (bool)dto.IsActive;
        if (dto.Note is not null) student.Note = dto.Note;
        if (dto.ResponsibleId is not null)
        {
            if (!await CheckResponsibleExistenceAsync((Guid)dto.ResponsibleId)) throw new BadRequestException("ResponsibleNotFound");

            student.ResponsibleId = dto.ResponsibleId;
        }

        await SetOptionalDataAsync(_context, student, dto.Document, dto.Contact, dto.Residence);

        if (dto.RemoveList is not null) student.RemoveProps(_context, dto.RemoveList);
    }

    public async Task DeleteAsync(Guid id)
    {
        Student student = await GetOneModelAsync(id);

        _context.Students.Remove(student);
    }

    public async Task<IEnumerable<GetAllStudentsDto>> GetAllAsync()
    {
        return await _context.Students
            .AsNoTracking()
            .Include("Responsible.Document")
            .Select(student => new GetAllStudentsDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                IsActive = student.IsActive,
                Document = student.Document,
                Contact = student.Contact,
                Note = student.Note,
                Residence = student.Residence,
                Responsible = student.Responsible
            })
            .ToListAsync();
    }

    public async Task<GetOneStudentDto> GetOneAsync(Guid id)
    {
        GetOneStudentDto? student = await _context.Students
            .AsNoTracking()
            .Where(student => student.Id == id)
            .Include("Responsible.Document")
            .Select(student => new GetOneStudentDto
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                IsActive = student.IsActive,
                Document = student.Document,
                Contact = student.Contact,
                Residence = student.Residence,
                Note = student.Note,
                Responsible = student.Responsible
            })
            .FirstOrDefaultAsync();

        if (student is null) throw new NotFoundException("StudentNotFound");

        return student;
    }

    private async Task<Student> GetOneModelAsync(Guid id)
    {
        Student? student = await _context.Students
            .Where(student => student.Id == id)
            .Include("Document")
            .Include("Responsible")
            .Include("Contact")
            .Include("Residence")
            .Include("Responsible.Document")
            .FirstOrDefaultAsync();

        if (student is null) throw new NotFoundException("StudentNotFound");

        return student;
    }

    private async Task SetOptionalDataAsync(
        SchoolContext context,
        Student student,
        UpdateDocumentDto? document,
        UpdateContactDto? contact,
        UpdateResidenceDto? residence)
    {
        if (document is not null)
        {
            if (document.Rg is not null)
                if (!await CheckStudentDocumentRgUniquenessAsync(document.Rg))
                    throw new ConflictException("NotUniqueStudentRg");

            if (document.Cpf is not null)
                if (!await CheckStudentDocumentCpfUniquenessAsync(document.Cpf))
                    throw new ConflictException("NotUniqueStudentCpf");

            await student.SetDocumentAsync(context, document, CheckStudentDocumentIdUniquenessAsync);
        }
        if (contact is not null)
        {
            if (contact.Email is not null)
                if (!await CheckEmailUniquenessAsync(contact.Email))
                    throw new ConflictException("NotUniqueEmail");

            await student.SetContactAsync(context, contact, CheckContactIdUniquenessAsync);
        }
        if (residence is not null) await student.SetResidenceAsync(context, residence, CheckResidenceIdUniquenessAsync);
    }

    private async Task<bool> CheckStudentIdUniquenessAsync(Guid id) => !await _context.Students.AnyAsync(student => student.Id == id);

    private async Task<bool> CheckStudentDocumentIdUniquenessAsync(Guid id) => !await _context.StudentDocuments.AnyAsync(document => document.Id == id);

    private async Task<bool> CheckContactIdUniquenessAsync(Guid id) => !await _context.Contacts.AnyAsync(contact => contact.Id == id);

    private async Task<bool> CheckResidenceIdUniquenessAsync(Guid id) => !await _context.Residences.AnyAsync(residence => residence.Id == id);

    private async Task<bool> CheckEmailUniquenessAsync(string email) => !await _context.Contacts.AnyAsync(contact => contact.Email == email);

    private async Task<bool> CheckStudentDocumentRgUniquenessAsync(string rg) => !await _context.StudentDocuments.AnyAsync(document => document.Rg == rg);

    private async Task<bool> CheckStudentDocumentCpfUniquenessAsync(string cpf) => !await _context.StudentDocuments.AnyAsync(document => document.Cpf == cpf);

    private async Task<bool> CheckResponsibleExistenceAsync(Guid id) => await _context.Responsibles.AnyAsync(responsible => responsible.Id == id);
}
