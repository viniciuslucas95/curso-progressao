using CursoProgressao.Server.Data;
using CursoProgressao.Server.Dto.Documents;
using CursoProgressao.Server.Dto.Responsibles;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Responsibles;

public class ResponsiblesService : IResponsiblesService
{
    private readonly SchoolContext _context;

    public ResponsiblesService(SchoolContext context) => _context = context;

    public async Task<Guid> CreateAsync(CreateResponsibleDto dto)
    {
        Responsible responsible = new(dto.FirstName, dto.LastName);

        while (!await CheckResponsibleIdUniquenessAsync(responsible.Id)) responsible = new(dto.FirstName, dto.LastName);

        if (!await CheckRgUniquenessAsync(dto.Document.Rg)) throw new ConflictException("NotUniqueResponsibleRg");
        if (!await CheckCpfUniquenessAsync(dto.Document.Cpf)) throw new ConflictException("NotUniqueResponsibleCpf");

        UpdateDocumentDto document = new()
        {
            Rg = dto.Document.Rg,
            Cpf = dto.Document.Cpf,
        };

        await responsible.SetDocumentAsync(document, CheckDocumentIdUniquenessAsync);

        _context.Responsibles.Add(responsible);

        return responsible.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateResponsibleDto dto)
    {
        Responsible responsible = await GetOneModelAsync(id);

        if (dto.FirstName is not null) responsible.FirstName = dto.FirstName;
        if (dto.LastName is not null) responsible.LastName = dto.LastName;
        if (dto.Document is not null)
        {
            if (dto.Document.Rg is not null)
                if (!await CheckRgUniquenessAsync(dto.Document.Rg)) throw new ConflictException("NotUniqueResponsibleRg");

            if (dto.Document.Cpf is not null)
                if (!await CheckCpfUniquenessAsync(dto.Document.Cpf)) throw new ConflictException("NotUniqueResponsibleCpf");

            await responsible.SetDocumentAsync(dto.Document, CheckDocumentIdUniquenessAsync);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        Responsible responsible = await GetOneModelAsync(id);

        // Check if there is student depending on it

        _context.Responsibles.Remove(responsible);
    }

    public async Task<IEnumerable<GetAllResponsiblesDto>> GetAllAsync()
    {
        return await _context.Responsibles
            .AsNoTracking()
            .Include(responsible => responsible.Document)
            .Select(responsible => new GetAllResponsiblesDto
            {
                Id = responsible.Id,
                FirstName = responsible.FirstName,
                LastName = responsible.LastName,
                Document = responsible.Document
            })
            .ToListAsync();
    }

    public async Task<GetOneResponsibleDto> GetOneAsync(Guid id)
    {
        GetOneResponsibleDto? responsible = await _context.Responsibles
            .AsNoTracking()
            .Where(responsible => responsible.Id == id)
            .Include(responsible => responsible.Document)
            .Select(responsible => new GetOneResponsibleDto
            {
                FirstName = responsible.FirstName,
                LastName = responsible.LastName,
                Document = responsible.Document
            })
            .FirstOrDefaultAsync();

        if (responsible is null) throw new BadRequestException("ResponsibleNotFound");

        return responsible;
    }

    private async Task<Responsible> GetOneModelAsync(Guid id)
    {
        Responsible? responsible = await _context.Responsibles
            .Where(responsible => responsible.Id == id)
            .Include(responsible => responsible.Document)
            .Include(responsible => responsible.Students)
            .FirstOrDefaultAsync();

        if (responsible is null) throw new BadRequestException("ResponsibleNotFound");

        return responsible;
    }

    private async Task<bool> CheckResponsibleIdUniquenessAsync(Guid id) => !await _context.Responsibles.AnyAsync(responsible => responsible.Id == id);
    private async Task<bool> CheckDocumentIdUniquenessAsync(Guid id) => !await _context.ResponsibleDocuments.AnyAsync(document => document.Id == id);
    private async Task<bool> CheckRgUniquenessAsync(string rg) => !await _context.ResponsibleDocuments.AnyAsync(document => document.Rg == rg);
    private async Task<bool> CheckCpfUniquenessAsync(string cpf) => !await _context.ResponsibleDocuments.AnyAsync(document => document.Cpf == cpf);
}
