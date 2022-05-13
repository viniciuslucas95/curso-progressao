using CursoProgressao.Api.Data.Contexts;
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
            .Include($"{nameof(Responsible)}.{nameof(Document)}")
            .Select(student => new GetAllStudentsDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Document = student.Document,
                Contact = student.Contact,
                Note = student.Note,
                Residence = student.Residence,
                Class = student.Class,
                Responsible = student.Responsible
            })
            .ToListAsync();
    }

    public async Task<GetOneStudentDto?> GetOneAsync(Guid id)
    {
        return await _context.Students
            .AsNoTracking()
            .Where(student => student.Id == id)
            .Include($"{nameof(Responsible)}.{nameof(Document)}")
            .Select(student => new GetOneStudentDto
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Document = student.Document,
                Contact = student.Contact,
                Residence = student.Residence,
                Class = student.Class,
                Note = student.Note,
                Responsible = student.Responsible
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Student?> GetOneModelAsync(Guid id)
    {
        return await _context.Students
            .Include(nameof(Document))
            .Include(nameof(Responsible))
            .Include(nameof(Contact))
            .Include(nameof(Residence))
            .Include(nameof(Class))
            .Include($"{nameof(Responsible)}.{nameof(Document)}")
            .FirstOrDefaultAsync(student => student.Id == id);
    }
}
