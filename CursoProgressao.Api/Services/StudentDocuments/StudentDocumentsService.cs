using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.StudentDocuments;

public class StudentDocumentsService : IStudentDocumentsService
{
    public void Update(Student student, ModifyDocumentDto dto)
    {
        if (dto.Rg is not null)
            student.Document.Rg = dto.Rg;
        if (dto.Cpf is not null)
            student.Document.Cpf = dto.Cpf;
    }
}
