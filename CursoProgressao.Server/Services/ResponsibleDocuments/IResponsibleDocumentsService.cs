using CursoProgressao.Shared.Dto.Documents;

namespace CursoProgressao.Server.Services.ResponsibleDocuments;

public interface IResponsibleDocumentsService
{
    Task<Guid> CreateAsync(Guid responsibleId, CreateDocumentDto dto);
    Task UpdateAsync(Guid responsibleId, UpdateDocumentDto dto);
    Task DeleteAsync(Guid responsibleId);
}
