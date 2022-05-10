using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.ResponsibleDocuments;

public interface IResponsibleDocumentsService
{
    void Update(Student student, ModifyDocumentDto dto);
}
