using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Contexts
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();

        public SchoolContext(DbContextOptions<SchoolContext> options, IConfiguration configuration) : base(options) { }
    }
}
