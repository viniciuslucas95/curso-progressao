using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Shared.Dto.Documents;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.StudentDocuments;

public class StudentDocumentsService : IStudentDocumentsService
{
    private readonly SchoolContext _context;
    private readonly BadRequestException _notFoundException = new("StudentDocumentNotFound");

    public StudentDocumentsService(SchoolContext context) => _context = context;

    public async Task<Guid> CreateAsync(Guid studentId, UpdateDocumentDto dto)
    {
        if (dto.Rg is not null)
            await AssessRgUniquenessAsync(dto.Rg);

        if (dto.Cpf is not null)
            await AssessCpfUniquenessAsync(dto.Cpf);

        StudentDocument document = new(studentId, dto.Cpf, dto.Rg);

        while (await DoesExistAsync(document.Id))
            document = new(studentId, dto.Cpf, dto.Rg);

        _context.StudentDocuments.Add(document);

        return document.Id;
    }

    public async Task UpdateAsync(Guid studentId, UpdateDocumentDto dto)
    {
        StudentDocument document = await GetModelAsync(studentId);

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
        StudentDocument document = await GetModelAsync(studentId);

        _context.StudentDocuments.Remove(document);
    }

    private async Task AssessRgUniquenessAsync(string rg)
    {
        bool doesExist = await _context.StudentDocuments.AnyAsync(document => document.Rg == rg);

        if (doesExist) throw new ConflictException("StudentRgAlreadyExists");
    }

    private async Task AssessCpfUniquenessAsync(string cpf)
    {
        bool doesExist = await _context.StudentDocuments.AnyAsync(document => document.Cpf == cpf);

        if (doesExist) throw new ConflictException("StudentCpfAlreadyExists");
    }

    private async Task<StudentDocument> GetModelAsync(Guid studentId)
    {
        StudentDocument? document = await _context.StudentDocuments
            .FirstOrDefaultAsync(document => document.StudentId == studentId);

        if (document is null) throw _notFoundException;

        return document;
    }

    private async Task<bool> DoesExistAsync(Guid id)
        => await _context.StudentDocuments.AnyAsync(document => document.Id == id);
}
