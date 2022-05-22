using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Server.Services.ResponsibleDocuments;
using CursoProgressao.Shared.Dto.Responsibles;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Responsibles;

public class ResponsiblesService : IResponsiblesService
{
    private readonly SchoolContext _context;
    private readonly IResponsibleDocumentsService _responsibleDocumentsService;
    private readonly NotFoundException _notFoundException = new("ResponsibleNotFound");

    public ResponsiblesService(SchoolContext context, IResponsibleDocumentsService responsibleDocumentsService)
    {
        _context = context;
        _responsibleDocumentsService = responsibleDocumentsService;
    }

    public async Task<Guid> CreateAsync(CreateResponsibleDto dto)
    {
        Responsible responsible = new(dto.FirstName, dto.LastName);

        while (await DoesExistAsync(responsible.Id))
            responsible = new(dto.FirstName, dto.LastName);

        await _responsibleDocumentsService.CreateAsync(responsible.Id, dto.Document);

        _context.Responsibles.Add(responsible);

        return responsible.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateResponsibleDto dto)
    {
        Responsible responsible = await GetModelAsync(id);

        if (dto.FirstName is not null) responsible.FirstName = dto.FirstName;
        if (dto.LastName is not null) responsible.LastName = dto.LastName;
        if (dto.Document is not null)
            await _responsibleDocumentsService.UpdateAsync(responsible.Id, dto.Document);
    }

    public async Task DeleteAsync(Guid id)
    {
        Responsible responsible = await GetCompleteModelAsync(id);

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

        if (responsible is null) throw new NotFoundException("ResponsibleNotFound");

        return responsible;
    }

    public async Task CheckExistenceAsync(Guid id)
    {
        bool doesExist = await _context.Responsibles.AnyAsync(responsible => responsible.Id == id);

        if (!doesExist) throw _notFoundException;
    }

    private async Task<Responsible> GetModelAsync(Guid id)
    {
        Responsible? responsible = await _context.Responsibles
            .Where(responsible => responsible.Id == id)
            .FirstOrDefaultAsync();

        if (responsible is null) throw _notFoundException;

        return responsible;
    }

    private async Task<Responsible> GetCompleteModelAsync(Guid id)
    {
        Responsible? responsible = await _context.Responsibles
            .Where(responsible => responsible.Id == id)
            .Include("Document")
            .Include("Students")
            .FirstOrDefaultAsync();

        if (responsible is null) throw _notFoundException;

        return responsible;
    }

    private async Task<bool> DoesExistAsync(Guid id)
        => await _context.Responsibles.AnyAsync(responsible => responsible.Id == id);
}
