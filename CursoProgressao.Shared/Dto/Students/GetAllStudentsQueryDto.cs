using CursoProgressao.Shared.Dto.Common;

namespace CursoProgressao.Shared.Dto.Students;

public class GetAllStudentsQueryDto : GetAllQueryDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Rg { get; set; }
    public string? Cpf { get; set; }
    public string? ResponsibleFirstName { get; set; }
    public string? ResponsibleLastName { get; set; }
    public string? ResponsibleRg { get; set; }
    public string? ResponsibleCpf { get; set; }
    public Guid? ClassId { get; set; }
    public bool? IsOwing { get; set; }
}
