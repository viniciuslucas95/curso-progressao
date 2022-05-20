using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Shared.Dto.Documents;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.ResponsibleDocuments;

public class ResponsibleDocumentsService : IResponsibleDocumentsService
{
    private readonly SchoolContext _context;
    private readonly BadRequestException _notFoundException = new("ResponsibleDocumentNotFound");

    public ResponsibleDocumentsService(SchoolContext context) => _context = context;

    public async Task<Guid> CreateAsync(Guid responsibleId, CreateDocumentDto dto)
    {
        if (dto.Rg is not null)
            await AssessRgUniquenessAsync(dto.Rg);

        await AssessCpfUniquenessAsync(dto.Cpf);

        ResponsibleDocument document = new(responsibleId, dto.Cpf, dto.Rg);

        while (await DoesExistAsync(document.Id))
            document = new(responsibleId, dto.Cpf, dto.Rg);

        _context.ResponsibleDocuments.Add(document);

        return document.Id;
    }

    public async Task UpdateAsync(Guid responsibleId, UpdateDocumentDto dto)
    {
        ResponsibleDocument document = await GetModelAsync(responsibleId);

        if (dto.Rg is not null)
        {
            await AssessRgUniquenessAsync(dto.Rg);
            document.Rg = dto.Rg;
        }

        if (dto.Cpf is not null)
        {
            await AssessCpfUniquenessAsync(dto.Cpf);
            document.Cpf = dto.Cpf;
        }
    }

    public async Task DeleteAsync(Guid studentId)
    {
        ResponsibleDocument document = await GetModelAsync(studentId);

        _context.ResponsibleDocuments.Remove(document);
    }

    private async Task<ResponsibleDocument> GetModelAsync(Guid responsibleId)
    {
        ResponsibleDocument? document = await _context.ResponsibleDocuments
            .FirstOrDefaultAsync(document => document.ResponsibleId == responsibleId);

        if (document is null) throw _notFoundException;

        return document;
    }

    private async Task AssessRgUniquenessAsync(string rg)
    {
        bool doesExist = await _context.ResponsibleDocuments.AnyAsync(document => document.Rg == rg);

        if (doesExist) throw new ConflictException("ResponsibleRgAlreadyExists");
    }

    private async Task AssessCpfUniquenessAsync(string cpf)
    {
        bool doesExist = await _context.ResponsibleDocuments.AnyAsync(document => document.Cpf == cpf);

        if (doesExist) throw new ConflictException("ResponsibleCpfAlreadyExists");
    }

    private async Task<bool> DoesExistAsync(Guid id)
        => await _context.ResponsibleDocuments.AnyAsync(document => document.Id == id);
}
