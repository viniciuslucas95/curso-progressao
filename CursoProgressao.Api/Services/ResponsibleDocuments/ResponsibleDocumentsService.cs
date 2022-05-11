using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.ResponsibleDocuments;

public class ResponsibleDocumentsService : IResponsibleDocumentsService
{
    public void Update(Student student, ModifyDocumentDto dto)
    {
        if (dto.Rg is not null)
            student.Responsible.Document.Rg = dto.Rg;
        if (dto.Cpf is not null)
            student.Responsible.Document.Cpf = dto.Cpf;
    }
}
