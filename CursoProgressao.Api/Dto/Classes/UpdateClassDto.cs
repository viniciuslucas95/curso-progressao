using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Classes;

public class UpdateClassDto
{
    [MinLength(2, ErrorMessage = "Class name must have at least 2 characters")]
    public string? Name { get; set; }
}
