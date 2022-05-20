using CursoProgressao.Server.Services.Classes;
using CursoProgressao.Server.Services.Contacts;
using CursoProgressao.Server.Services.Contracts;
using CursoProgressao.Server.Services.Payments;
using CursoProgressao.Server.Services.Residences;
using CursoProgressao.Server.Services.ResponsibleDocuments;
using CursoProgressao.Server.Services.Responsibles;
using CursoProgressao.Server.Services.StudentDocuments;
using CursoProgressao.Server.Services.Students;

namespace CursoProgressao.Server.Test.Fixtures;

public class StudentsFixture : SchoolDbFixture
{
    public Guid Id { get; set; }
    public StudentsService Service { get; private init; }

    public StudentsFixture() : base()
    {
        ClassesService classesService = new(Context);
        ResponsibleDocumentsService responsibleDocumentsService = new(Context);
        ContractsService contractsService = new(Context, classesService);

        Id = Guid.Empty;
        Service = new(
            Context,
            contractsService,
            classesService,
            new ResponsiblesService(Context, responsibleDocumentsService),
            new PaymentsService(Context, contractsService),
            new ContactsService(Context),
            new ResidencesService(Context),
            new StudentDocumentsService(Context)
            );
    }
}
