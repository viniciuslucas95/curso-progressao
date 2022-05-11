using CursoProgressao.Api.Data.Contexts;
using CursoProgressao.Api.Dto.Responsibles;
using CursoProgressao.Api.Dto.Students;
using CursoProgressao.Api.Models;
using CursoProgressao.Api.Models.Documents;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Api.Repositories.Students;

public class StudentsRepository : IStudentsRepository
{
    private readonly SchoolContext _context;

    public StudentsRepository(SchoolContext context) => _context = context;

    public void Create(Student student) => _context.Students.Add(student);

    public void Delete(Student student) => _context.Students.Remove(student);

    public async Task<IEnumerable<GetAllStudentsDto>> GetAllAsync()
    {
        return await _context.Students
            .AsNoTracking()
            .Select(student => new GetAllStudentsDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Document = student.Document,
                Contact = student.Contact,
                Note = student.Note,
                Residence = student.Residence,
                Class = GetClassName(student.Class),
                Responsible = GetResponsible(student.Responsible)
            })
            .ToListAsync();
    }

    public async Task<GetOneStudentDto?> GetOneAsync(Guid id)
    {
        return await _context.Students
            .AsNoTracking()
            .Where(student => student.Id == id)
            .Select(student => new GetOneStudentDto
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Document = student.Document,
                Contact = student.Contact,
                Residence = student.Residence,
                Class = GetClassName(student.Class),
                Note = student.Note,
                Responsible = GetResponsible(student.Responsible)
            })
            .SingleOrDefaultAsync();
    }

    public async Task<Student?> GetOneModelAsync(Guid id)
    {
        return await _context.Students
            .Where(student => student.Id == id)
            .Include(nameof(Document))
            .Include(nameof(Responsible))
            .Include(nameof(Contact))
            .Include(nameof(Residence))
            .Include(nameof(Class))
            .Include($"{nameof(Responsible)}.{nameof(Document)}")
            .SingleOrDefaultAsync();
    }

    private static string? GetClassName(Class? classObj)
        => classObj is not null ? classObj.Name : null;

    private static GetOneResponsibleDto GetResponsible(Responsible responsible)
        => new()
        {
            FirstName = responsible.FirstName,
            LastName = responsible.LastName,
            Document = responsible.Document
        };
}
