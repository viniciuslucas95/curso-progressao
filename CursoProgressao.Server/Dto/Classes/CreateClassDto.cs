using CursoProgressao.Server.Attributes;

namespace CursoProgressao.Server.Dto.Classes;

public class CreateClassDto
{
    [CustomRequired]
    [CustomMinLengthAttribute(2)]
    public string Name { get; set; } = null!;
}
