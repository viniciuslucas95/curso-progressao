using CursoProgressao.Api.Dto.Contacts;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Contacts;

public class ContactsService : IContactsService
{
    public void Update(Student student, ModifyContactDto dto)
    {
        if (dto.Email is not null)
            student.Contact.Email = dto.Email;
        if (dto.CellPhone is not null)
            student.Contact.CellPhone = dto.CellPhone;
        if (dto.Landline is not null)
            student.Contact.Landline = dto.Landline;
    }
}
