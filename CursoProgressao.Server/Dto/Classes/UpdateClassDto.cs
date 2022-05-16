using CursoProgressao.Server.Attributes;

namespace CursoProgressao.Server.Dto.Classes;

public class UpdateClassDto
{
    [CustomMinLengthAttribute(2)]
    public string Name { get; set; } = null!;
}
