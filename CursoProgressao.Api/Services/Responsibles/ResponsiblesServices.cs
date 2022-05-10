using CursoProgressao.Api.Dto.Responsibles;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Responsibles;

public class ResponsiblesServices : IResponsiblesService
{
    public void Update(Student student, ModifyResponsibleDto dto)
    {
        student.Responsible.FirstName = dto.FirstName ?? student.Responsible.FirstName;
        student.Responsible.LastName = dto.LastName ?? student.Responsible.LastName;
    }
}
