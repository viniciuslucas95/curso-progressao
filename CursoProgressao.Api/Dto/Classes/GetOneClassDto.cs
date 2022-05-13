using CursoProgressao.Api.Dto.Students;

namespace CursoProgressao.Api.Dto.Classes;

public class GetOneClassDto
{
    public string Name { get; set; } = null!;
    public IEnumerable<GetAllStudentsDto> Students { get; set; } = null!;
}
