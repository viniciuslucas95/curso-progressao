using CursoProgressao.Api.Models;
using CursoProgressao.Api.Models.Documents;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Api.Data.Contexts
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<StudentDocument> StudentDocuments => Set<StudentDocument>();
        public DbSet<Responsible> Responsibles => Set<Responsible>();
        public DbSet<ResponsibleDocument> ResponsibleDocuments => Set<ResponsibleDocument>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Residence> Residences => Set<Residence>();

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
    }
}
