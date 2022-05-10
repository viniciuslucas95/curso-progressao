using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.StudentDocuments;

public interface IStudentDocumentsService
{
    void Update(Student student, ModifyDocumentDto dto);
}
