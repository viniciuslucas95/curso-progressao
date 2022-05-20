using CursoProgressao.Shared.Dto.Documents;

namespace CursoProgressao.Server.Services.StudentDocuments;

public interface IStudentDocumentsService
{
    Task<Guid> CreateAsync(Guid studentId, UpdateDocumentDto dto);
    Task UpdateAsync(Guid studentId, UpdateDocumentDto dto);
    Task DeleteAsync(Guid studentId);
}
