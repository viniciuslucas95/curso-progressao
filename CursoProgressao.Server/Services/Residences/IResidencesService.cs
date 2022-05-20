using CursoProgressao.Shared.Dto.Residences;

namespace CursoProgressao.Server.Services.Residences;

public interface IResidencesService
{
    Task<Guid> CreateAsync(Guid studentId, UpdateResidenceDto dto);
    Task UpdateAsync(Guid studentId, UpdateResidenceDto dto);
    Task DeleteAsync(Guid studentId);
}
