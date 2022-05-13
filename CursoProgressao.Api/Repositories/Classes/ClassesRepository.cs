using CursoProgressao.Api.Data.Contexts;
using CursoProgressao.Api.Dto.Classes;
using CursoProgressao.Api.Dto.Students;
using CursoProgressao.Api.Models;
using CursoProgressao.Api.Models.Documents;
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
            .Include($"Students.{nameof(Document)}")
            .Include($"Students.{nameof(Residence)}")
            .Include($"Students.{nameof(Contact)}")
            .Include($"Students.{nameof(Responsible)}")
            .Include($"Students.{nameof(Responsible)}.{nameof(Document)}")
            .Select(classObj => new GetAllClassesDto
            {
                Id = classObj.Id,
                Name = classObj.Name,
                Students = GetStudents(classObj.Students)
            })
            .ToListAsync();
    }

    public async Task<GetOneClassDto?> GetOneAsync(Guid id)
    {
        return await _context.Classes
            .AsNoTracking()
            .Where(classObj => classObj.Id == id)
            .Include($"Students.{nameof(Document)}")
            .Include($"Students.{nameof(Residence)}")
            .Include($"Students.{nameof(Contact)}")
            .Include($"Students.{nameof(Responsible)}")
            .Include($"Students.{nameof(Responsible)}.{nameof(Document)}")
            .Select(classObj => new GetOneClassDto
            {
                Name = classObj.Name,
                Students = GetStudents(classObj.Students)
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Class?> GetOneModelAsync(Guid id)
    {
        return await _context.Classes
            .Include(classObj => classObj.Students)
            .FirstOrDefaultAsync(classObj => classObj.Id == id);
    }

    private static IEnumerable<GetAllStudentsDto> GetStudents(IReadOnlyCollection<Student> students)
        => students
        .Select(student => new GetAllStudentsDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Contact = student.Contact,
            Document = student.Document,
            Note = student.Note,
            Residence = student.Residence,
            Responsible = student.Responsible
        });
}
