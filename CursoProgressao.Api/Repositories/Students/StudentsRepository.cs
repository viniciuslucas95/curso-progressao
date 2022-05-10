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
                Responsible = new GetOneResponsibleDto
                {
                    FirstName = student.Responsible.FirstName,
                    LastName = student.Responsible.LastName,
                    Document = student.Responsible.Document
                }
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
                Responsible = new GetOneResponsibleDto
                {
                    FirstName = student.Responsible.FirstName,
                    LastName = student.Responsible.LastName,
                    Document = student.Responsible.Document
                }
            })
            .SingleOrDefaultAsync();
    }

    public async Task<Student?> GetOneModelAsync(Guid id)
    {
        return await _context.Students
            .Where(student => student.Id == id)
            .Include(nameof(Document))
            .Include(nameof(Responsible))
            .Include($"{nameof(Responsible)}.{nameof(Document)}")
            .SingleOrDefaultAsync();
    }
}
