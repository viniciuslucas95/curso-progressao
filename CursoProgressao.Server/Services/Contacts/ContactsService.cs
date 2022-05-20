using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Shared.Dto.Contacts;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Contacts;

public class ContactsService : IContactsService
{
    private readonly SchoolContext _context;
    private readonly NotFoundException _notFoundException = new("ContactNotFound");

    public ContactsService(SchoolContext context) => _context = context;

    public async Task<Guid> CreateAsync(Guid studentId, UpdateContactDto dto)
    {
        if (dto.Email is not null)
            await AssessEmailUniquenessAsync(dto.Email);

        Contact contact = new(studentId, dto.Email, dto.Landline, dto.CellPhone);

        while (await DoesExistAsync(contact.Id))
            contact = new(studentId, dto.Email, dto.Landline, dto.CellPhone);

        _context.Contacts.Add(contact);

        return contact.Id;
    }

    public async Task UpdateAsync(Guid studentId, UpdateContactDto dto)
    {
        Contact contact = await GetModelAsync(studentId);

        if (dto.Email is not null)
        {
            await AssessEmailUniquenessAsync(dto.Email);
            contact.Email = dto.Email;
        }

        if (dto.CellPhone is not null) contact.CellPhone = dto.CellPhone;
        if (dto.Landline is not null) contact.Landline = dto.Landline;
    }

    public async Task DeleteAsync(Guid studentId)
    {
        Contact contact = await GetModelAsync(studentId);

        _context.Contacts.Remove(contact);
    }

    private async Task AssessEmailUniquenessAsync(string email)
    {
        bool doesExist = await _context.Contacts.AnyAsync(contact => contact.Email == email);

        if (doesExist) throw new ConflictException("EmailAlreadyExists");
    }

    private async Task<Contact> GetModelAsync(Guid studentId)
    {
        Contact? contact = await _context.Contacts
            .FirstOrDefaultAsync(contact => contact.StudentId == studentId);

        if (contact is null) throw _notFoundException;

        return contact;
    }

    private async Task<bool> DoesExistAsync(Guid id)
        => await _context.Contacts.AnyAsync(contact => contact.Id == id);
}
