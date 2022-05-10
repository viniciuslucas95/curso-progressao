using CursoProgressao.Api.Dto.Contacts;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Contacts;

public interface IContactsService
{
    void Update(Student student, ModifyContactDto dto);
}
