using CursoProgressao.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Test.Fixtures;

public class SchoolDbFixture : IDisposable
{
    public SchoolContext Context { get; private init; }

    public SchoolDbFixture()
    {
        DbContextOptionsBuilder<SchoolContext> optionsBuilder = new();
        optionsBuilder.UseSqlite("Data Source=:memory:");
        SchoolContext context = new(optionsBuilder.Options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        Context = context;
    }

    public void Dispose() => GC.SuppressFinalize(this);
}
