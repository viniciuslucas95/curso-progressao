using CursoProgressao.Server.Services.ResponsibleDocuments;
using CursoProgressao.Server.Services.Responsibles;

namespace CursoProgressao.Server.Test.Fixtures;

public class ResponsiblesFixture : SchoolDbFixture
{
    public Guid Id { get; set; }
    public ResponsiblesService Service { get; private init; }

    public ResponsiblesFixture() : base()
    {
        Id = Guid.Empty;
        Service = new(Context, new ResponsibleDocumentsService(Context));
    }
}
