using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Dto.Classes;

public class GetOneClassDto
{
    public string Name { get; set; } = null!;
    public IEnumerable<Student> Students { get; set; } = null!;
}
