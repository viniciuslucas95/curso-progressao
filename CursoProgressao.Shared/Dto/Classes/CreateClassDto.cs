using CursoProgressao.Shared.Attributes;

namespace CursoProgressao.Shared.Dto.Classes;

public class CreateClassDto
{
    [Required]
    [MinLength(2)]
    public string Name { get; set; } = null!;
}
