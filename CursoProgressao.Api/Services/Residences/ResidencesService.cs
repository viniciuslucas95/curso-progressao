using CursoProgressao.Api.Dto.Residences;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Residences;

public class ResidencesService : IResidencesService
{
    public void Update(Student student, ModifyResidenceDto dto)
    {
        if (dto.ZipCode is not null)
            student.Residence.ZipCode = dto.ZipCode;
        if (dto.Address is not null)
            student.Residence.Address = dto.Address;
    }
}
