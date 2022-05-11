using CursoProgressao.Api.Attributes;

namespace CursoProgressao.Api.Dto.Classes;

public class UpdateClassDto
{
    [CustomMinLengthAttribute(2)]
    public string? Name { get; set; }
}
