using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Shared.Dto.Contacts;
using CursoProgressao.Shared.Dto.Documents;
using CursoProgressao.Shared.Dto.Residences;
using CursoProgressao.Shared.Dto.Students;
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

        while (await CheckStudentExistenceAsync(student.Id)) student = new(dto.FirstName, dto.LastName, dto.Note, dto.ResponsibleId);

        UpdateDocumentDto? document = dto.Document is not null ? new()
        {
            Rg = dto.Document.Rg,
            Cpf = dto.Document.Cpf
        } : null;

        await SetOptionalDataAsync(student, document, dto.Contact, dto.Residence);

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

        await SetOptionalDataAsync(student, dto.Document, dto.Contact, dto.Residence);

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
        Student student,
        UpdateDocumentDto? document,
        UpdateContactDto? contact,
        UpdateResidenceDto? residence)
    {
        if (document is not null)
        {
            if (document.Rg is not null)
                if (await CheckDocumentRgExistenceAsync(document.Rg))
                    throw new ConflictException("NotUniqueStudentRg");

            if (document.Cpf is not null)
                if (await CheckDocumentCpfExistenceAsync(document.Cpf))
                    throw new ConflictException("NotUniqueStudentCpf");

            await student.SetDocumentAsync(document, AddDocument, CheckDocumentExistenceAsync);
        }
        if (contact is not null)
        {
            if (contact.Email is not null)
                if (await CheckEmailExistenceAsync(contact.Email))
                    throw new ConflictException("NotUniqueEmail");

            await student.SetContactAsync(contact, AddContact, CheckContactExistenceAsync);
        }
        if (residence is not null)
            await student.SetResidenceAsync(residence, AddResidence, CheckResidenceExistenceAsync);
    }

    private void AddDocument(StudentDocument document) => _context.StudentDocuments.Add(document);
    private void AddContact(Contact contact) => _context.Contacts.Add(contact);
    private void AddResidence(Residence residence) => _context.Residences.Add(residence);
    private async Task<bool> CheckDocumentExistenceAsync(Guid id) => await _context.StudentDocuments.AnyAsync(document => document.Id == id);
    private async Task<bool> CheckContactExistenceAsync(Guid id) => await _context.Contacts.AnyAsync(contact => contact.Id == id);
    private async Task<bool> CheckResidenceExistenceAsync(Guid id) => await _context.Residences.AnyAsync(residence => residence.Id == id);
    private async Task<bool> CheckStudentExistenceAsync(Guid id) => await _context.Students.AnyAsync(student => student.Id == id);
    private async Task<bool> CheckEmailExistenceAsync(string email) => await _context.Contacts.AnyAsync(contact => contact.Email == email);
    private async Task<bool> CheckDocumentRgExistenceAsync(string rg) => await _context.StudentDocuments.AnyAsync(document => document.Rg == rg);
    private async Task<bool> CheckDocumentCpfExistenceAsync(string cpf) => await _context.StudentDocuments.AnyAsync(document => document.Cpf == cpf);
    private async Task<bool> CheckResponsibleExistenceAsync(Guid id) => await _context.Responsibles.AnyAsync(responsible => responsible.Id == id);
}
