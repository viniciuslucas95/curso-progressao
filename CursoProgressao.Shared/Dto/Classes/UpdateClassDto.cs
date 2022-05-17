using CursoProgressao.Shared.Attributes;

namespace CursoProgressao.Shared.Dto.Classes;

public class UpdateClassDto
{
    [CustomMinLengthAttribute(2)]
    public string Name { get; set; } = null!;
}
