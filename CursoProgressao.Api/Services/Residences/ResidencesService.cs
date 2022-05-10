using CursoProgressao.Api.Dto.Residences;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Residences;

public class ResidencesService : IResidencesService
{
    public void Update(Student student, ModifyResidenceDto dto)
    {
        student.Residence.ZipCode = dto.ZipCode ?? student.Residence.ZipCode;
        student.Residence.Address = dto.Address ?? student.Residence.Address;
    }
}
