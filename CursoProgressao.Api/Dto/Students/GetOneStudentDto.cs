using CursoProgressao.Api.Dto.Contacts;
using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Dto.Residences;
using CursoProgressao.Api.Dto.Responsibles;

namespace CursoProgressao.Api.Dto.Students
{
    public class GetOneStudentDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Note { get; set; }
        public GetOneDocumentDto Document { get; set; } = null!;
        public GetOneResponsibleDto Responsible { get; set; } = null!;
        public GetOneContactDto Contact { get; set; } = null!;
        public GetOneResidenceDto Residence { get; set; } = null!;
    }
}
