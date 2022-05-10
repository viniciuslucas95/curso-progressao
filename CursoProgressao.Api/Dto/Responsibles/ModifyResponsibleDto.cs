using CursoProgressao.Api.Dto.Documents;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Responsibles;

public class ModifyResponsibleDto
{
    [MinLength(2, ErrorMessage = "First name must have at least 2 characters")]
    public string? FirstName { get; set; } = null!;
    [MinLength(2, ErrorMessage = "Last name must have at least 2 characters")]
    public string? LastName { get; set; } = null!;
    public ModifyDocumentDto? Document { get; set; }
}
