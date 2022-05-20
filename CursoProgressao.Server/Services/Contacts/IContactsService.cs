using CursoProgressao.Shared.Dto.Contacts;

namespace CursoProgressao.Server.Services.Contacts;

public interface IContactsService
{
    Task<Guid> CreateAsync(Guid studentId, UpdateContactDto dto);
    Task UpdateAsync(Guid studentId, UpdateContactDto dto);
    Task DeleteAsync(Guid studentId);
}
