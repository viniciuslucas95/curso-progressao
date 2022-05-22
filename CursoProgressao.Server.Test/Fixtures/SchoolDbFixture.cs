using CursoProgressao.Server.Data;
using CursoProgressao.Server.Services.Classes;
using CursoProgressao.Server.Services.Contacts;
using CursoProgressao.Server.Services.Contracts;
using CursoProgressao.Server.Services.Payments;
using CursoProgressao.Server.Services.Residences;
using CursoProgressao.Server.Services.ResponsibleDocuments;
using CursoProgressao.Server.Services.Responsibles;
using CursoProgressao.Server.Services.StudentDocuments;
using CursoProgressao.Server.Services.Students;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Test.Fixtures;

public class SchoolDbFixture : IDisposable
{
    public SchoolContext Context { get; private init; }
    public IClassesService ClassesService { get; private init; }
    public IResponsibleDocumentsService ResponsibleDocumentsService { get; private init; }
    public IResponsiblesService ResponsiblesService { get; private init; }
    public IContractsService ContractsService { get; private init; }
    public IContactsService ContactsService { get; private init; }
    public IResidencesService ResidencesService { get; private init; }
    public IStudentsService StudentsService { get; private init; }
    public IPaymentsService PaymentsService { get; private init; }
    public IStudentDocumentsService StudentDocumentsService { get; private init; }
    public Guid ClassId { get; set; }
    public Guid ResponsibleId { get; set; }
    public Guid StudentId { get; set; }

    public SchoolDbFixture()
    {
        DbContextOptionsBuilder<SchoolContext> optionsBuilder = new();
        optionsBuilder.UseSqlite("Data Source=:memory:");
        Context = new(optionsBuilder.Options);
        Context.Database.OpenConnection();
        Context.Database.EnsureCreated();

        ClassesService = new ClassesService(Context);
        ResponsibleDocumentsService = new ResponsibleDocumentsService(Context);
        ResponsiblesService = new ResponsiblesService(Context, ResponsibleDocumentsService);
        ContractsService = new ContractsService(Context, ClassesService);
        ContactsService = new ContactsService(Context);
        ResidencesService = new ResidencesService(Context);
        PaymentsService = new PaymentsService(Context, ContractsService);
        StudentDocumentsService = new StudentDocumentsService(Context);
        StudentsService = new StudentsService(
            Context,
            ContractsService,
            ClassesService,
            ResponsiblesService,
            PaymentsService,
            ContactsService,
            ResidencesService,
            StudentDocumentsService);
    }

    public void Dispose()
    {
        Context.Database.CloseConnection();
        GC.SuppressFinalize(this);
    }
}
