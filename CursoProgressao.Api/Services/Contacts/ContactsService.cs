using CursoProgressao.Api.Dto.Contacts;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Contacts;

public class ContactsService : IContactsService
{
    public void Update(Student student, ModifyContactDto dto)
    {
        student.Contact.Email = dto.Email ?? student.Contact.Email;
        student.Contact.CellPhone = dto.CellPhone ?? student.Contact.CellPhone;
        student.Contact.Landline = dto.Landline ?? student.Contact.Landline;
    }
}
