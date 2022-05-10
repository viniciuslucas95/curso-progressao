using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.ResponsibleDocuments;

public class ResponsibleDocumentsService : IResponsibleDocumentsService
{
    public void Update(Student student, ModifyDocumentDto dto)
    {
        student.Responsible.Document.Rg = dto.Rg ?? student.Responsible.Document.Rg;
        student.Responsible.Document.Cpf = dto.Cpf ?? student.Responsible.Document.Cpf;
    }
}
