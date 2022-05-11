using CursoProgressao.Api.Attributes;

namespace CursoProgressao.Api.Dto.Classes;

public class CreateClassDto
{
    [CustomRequired]
    public string Name { get; set; } = null!;
}
