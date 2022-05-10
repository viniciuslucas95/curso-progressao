using CursoProgressao.Api.Dto.Residences;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Residences;

public interface IResidencesService
{
    void Update(Student student, ModifyResidenceDto dto);
}
