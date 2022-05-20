using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Shared.Dto.Residences;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Residences;

public class ResidencesService : IResidencesService
{
    private readonly SchoolContext _context;
    private readonly NotFoundException _notFoundException = new("ResidenceNotFound");

    public ResidencesService(SchoolContext context) => _context = context;

    public async Task<Guid> CreateAsync(Guid studentId, UpdateResidenceDto dto)
    {
        Residence residence = new(studentId, dto.ZipCode, dto.Address);

        while (await DoesExistAsync(residence.Id))
            residence = new(studentId, dto.ZipCode, dto.Address);

        _context.Residences.Add(residence);

        return residence.Id;
    }

    public async Task UpdateAsync(Guid studentId, UpdateResidenceDto dto)
    {
        Residence residence = await GetModelAsync(studentId);

        if (dto.ZipCode is not null) residence.ZipCode = dto.ZipCode;
        if (dto.Address is not null) residence.Address = dto.Address;
    }

    public async Task DeleteAsync(Guid studentId)
    {
        Residence residence = await GetModelAsync(studentId);

        _context.Residences.Remove(residence);
    }

    private async Task<Residence> GetModelAsync(Guid studentId)
    {
        Residence? residence = await _context.Residences
            .FirstOrDefaultAsync(residence => residence.StudentId == studentId);

        if (residence is null) throw _notFoundException;

        return residence;
    }

    private async Task<bool> DoesExistAsync(Guid id)
        => await _context.Residences.AnyAsync(residence => residence.Id == id);
}
