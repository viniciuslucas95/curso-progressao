using CursoProgressao.Shared.Attributes;

namespace CursoProgressao.Shared.Dto.Classes;

public class CreateClassDto
{
    [CustomRequired]
    [CustomMinLengthAttribute(2)]
    public string Name { get; set; } = null!;
}
