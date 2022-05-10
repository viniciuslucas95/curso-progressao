using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.StudentDocuments;

public class StudentDocumentsService : IStudentDocumentsService
{
    public void Update(Student student, ModifyDocumentDto dto)
    {
        student.Document.Rg = dto.Rg ?? student.Document.Rg;
        student.Document.Cpf = dto.Cpf ?? student.Document.Cpf;
    }
}
