using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Shared.Dto.Classes;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Classes;

public class ClassesService : IClassesService
{
    private readonly SchoolContext _context;

    public ClassesService(SchoolContext context) => _context = context;

    public async Task<Guid> CreateAsync(CreateClassDto dto)
    {
        if (!await CheckNameUniquenessAsync(dto.Name)) throw new ConflictException("NotUniqueClassName");

        Class classObj = new(dto.Name);

        while (!await CheckIdUniquenessAsync(classObj.Id)) classObj = new(dto.Name);

        _context.Classes.Add(classObj);

        return classObj.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateClassDto dto)
    {
        Class classObj = await GetOneModelAsync(id);

        if (!await CheckNameUniquenessAsync(dto.Name)) throw new ConflictException("NotUniqueClassName");

        if (dto.Name is not null)
            classObj.Name = dto.Name;
    }

    public async Task DeleteAsync(Guid id)
    {
        Class classObj = await GetOneModelAsync(id);

        // Check if there is student depending on it

        _context.Classes.Remove(classObj);
    }

    public async Task<IEnumerable<GetAllClassesDto>> GetAllAsync()
    {
        return await _context.Classes
            .AsNoTracking()
            .Select(classObj => new GetAllClassesDto
            {
                Id = classObj.Id,
                Name = classObj.Name
            })
            .ToListAsync();
    }

    public async Task<GetOneClassDto> GetOneAsync(Guid id)
    {
        GetOneClassDto? classObj = await _context.Classes
            .AsNoTracking()
            .Where(classObj => classObj.Id == id)
            .Select(classObj => new GetOneClassDto
            {
                Name = classObj.Name
            })
            .FirstOrDefaultAsync();

        if (classObj is null) throw new BadRequestException("ClassNotFound");

        return classObj;
    }

    private async Task<Class> GetOneModelAsync(Guid id)
    {
        Class? classObj = await _context.Classes
            .Where(classObj => classObj.Id == id)
            .Include(classObj => classObj.Students)
            .FirstOrDefaultAsync();

        if (classObj is null) throw new BadRequestException("ClassNotFound");

        return classObj;
    }

    private async Task<bool> CheckIdUniquenessAsync(Guid id) => !await _context.Classes.AnyAsync(classObj => classObj.Id == id);
    private async Task<bool> CheckNameUniquenessAsync(string name) => !await _context.Classes.AnyAsync(classObj => classObj.Name == name);
}
