using CursoProgressao.Api.Dto.Documents;

namespace CursoProgressao.Api.Dto.Responsibles
{
    public class GetOneResponsibleDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public GetOneDocumentDto Document { get; set; } = null!;
    }
}
