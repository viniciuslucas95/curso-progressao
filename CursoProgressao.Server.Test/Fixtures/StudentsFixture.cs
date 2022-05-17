using CursoProgressao.Server.Services.Students;

namespace CursoProgressao.Server.Test.Fixtures;

public class StudentsFixture : SchoolDbFixture
{
    public Guid Id { get; set; }
    public StudentsService Service { get; private init; }

    public StudentsFixture() : base()
    {
        Id = Guid.Empty;
        Service = new(Context);
    }
}
