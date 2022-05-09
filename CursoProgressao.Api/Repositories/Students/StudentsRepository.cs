using Api.Data.Contexts;
using Api.Dto.Students;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Students
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly SchoolContext _context;

        public StudentsRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetAllStudentsDto>> GetAllAsync()
        {
            return await _context.Students
                .Select(student => new GetAllStudentsDto { Id = student.Id, Name = student.Name })
                .ToListAsync();
        }

        public async Task<GetOneStudentDto?> GetOneAsync(Guid id)
        {
            return await _context.Students
                .Where(student => student.Id == id)
                .Select(student => new GetOneStudentDto { Name = student.Name })
                .SingleOrDefaultAsync();
        }
    }
}
