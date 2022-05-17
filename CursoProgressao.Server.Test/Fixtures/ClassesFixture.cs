using CursoProgressao.Server.Services.Classes;

namespace CursoProgressao.Server.Test.Fixtures;

public class ClassesFixture : SchoolDbFixture
{
    public Guid Id { get; set; }
    public ClassesService Service { get; private init; }

    public ClassesFixture() : base()
    {
        Id = Guid.Empty;
        Service = new(Context);
    }
}
