using CursoProgressao.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Data;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentDocument> StudentDocuments => Set<StudentDocument>();
    public DbSet<Responsible> Responsibles => Set<Responsible>();
    public DbSet<ResponsibleDocument> ResponsibleDocuments => Set<ResponsibleDocument>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Residence> Residences => Set<Residence>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Error> Errors => Set<Error>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Payment> Payments => Set<Payment>();

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
}
