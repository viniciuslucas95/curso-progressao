using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Models;

public class Class : Model
{
    [Required(ErrorMessage = "Class name cannot be empty or null")]
    [MinLength(2, ErrorMessage = "Class name must have at least 2 characters")]
    public string Name { get; private set; }
    public List<Student> Students { get; private set; }

    public Class(string name)
    {
        Students = new List<Student>();
        Name = name;
    }
}
