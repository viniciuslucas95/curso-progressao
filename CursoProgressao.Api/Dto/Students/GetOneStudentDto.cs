using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Dto.Responsibles;

namespace CursoProgressao.Api.Dto.Students
{
    public class GetOneStudentDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public GetOneDocumentDto Document { get; set; } = null!;
        public GetOneResponsibleDto Responsible { get; set; } = null!;
    }
}
