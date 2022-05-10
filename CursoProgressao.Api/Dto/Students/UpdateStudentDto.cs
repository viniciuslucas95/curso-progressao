using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Dto.Responsibles;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Students;

public class UpdateStudentDto
{
    [MinLength(2, ErrorMessage = "First name must have at least 2 characters")]
    public string? FirstName { get; set; }
    [MinLength(2, ErrorMessage = "Last name must have at least 2 characters")]
    public string? LastName { get; set; }
    public bool? IsActive { get; set; }
    public ModifyDocumentDto? Document { get; set; }
    public ModifyResponsibleDto? Responsible { get; set; }
}
