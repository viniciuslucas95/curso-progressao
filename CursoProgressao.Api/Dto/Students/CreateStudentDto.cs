using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Students
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Name cannot be empty or null")]
        [MinLength(2, ErrorMessage = "Name must have at least 2 characters")]
        public string Name { get; set; } = null!;
    }
}
