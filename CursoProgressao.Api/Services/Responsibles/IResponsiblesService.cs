using CursoProgressao.Api.Dto.Responsibles;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Responsibles;

public interface IResponsiblesService
{
    void Update(Student student, ModifyResponsibleDto dto);
}
