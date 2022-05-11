using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Classes;

public class CreateClassDto
{
    [Required(ErrorMessage = "Class name cannot be empty or null")]
    [MinLength(2, ErrorMessage = "Class name must have at least 2 characters")]
    public string Name { get; set; } = null!;
}
