using CursoProgressao.Api.Data.Contexts;
using CursoProgressao.Api.Dto.Classes;
using CursoProgressao.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Api.Repositories.Classes;

public class ClassesRepository : IClassesRepository
{
    private readonly SchoolContext _context;

    public ClassesRepository(SchoolContext context) => _context = context;

    public void Create(Class classDto) => _context.Classes.Add(classDto);

    public void Delete(Class classDto) => _context.Classes.Remove(classDto);

    public async Task<IEnumerable<GetAllClassesDto>> GetAllAsync()
    {
        return await _context.Classes
            .AsNoTracking()
            .Select(classObj => new GetAllClassesDto
            {
                Id = classObj.Id,
                Name = classObj.Name,
                Students = classObj.Students
            })
            .ToListAsync();
    }

    public async Task<GetOneClassDto?> GetOneAsync(Guid id)
    {
        return await _context.Classes
            .AsNoTracking()
            .Where(classObj => classObj.Id == id)
            .Select(classObj => new GetOneClassDto
            {
                Name = classObj.Name,
                Students = classObj.Students
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Class?> GetOneModelAsync(Guid id)
    {
        return await _context.Classes
            .Where(classObj => classObj.Id == id)
            .Include(classObj => classObj.Students)
            .FirstOrDefaultAsync();
    }
}
