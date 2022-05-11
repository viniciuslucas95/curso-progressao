using CursoProgressao.Api.Dto.Responsibles;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Responsibles;

public class ResponsiblesServices : IResponsiblesService
{
    public void Update(Student student, ModifyResponsibleDto dto)
    {
        if (dto.FirstName is not null)
            student.Responsible.FirstName = dto.FirstName;
        if (dto.LastName is not null)
            student.Responsible.LastName = dto.LastName;
    }
}
