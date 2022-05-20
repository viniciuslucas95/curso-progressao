using CursoProgressao.Shared.Utils;

namespace CursoProgressao.Server.Models;

public abstract class Model
{
    public Guid Id { get; private init; }
    public DateTime CreatedAt { get; private init; }
    public DateTime UpdatedAt { get; set; }

    public Model()
    {
        Id = Guid.NewGuid();
        CreatedAt = CreatedAt.GetCurrentUtcTime();
        UpdatedAt = UpdatedAt.GetCurrentUtcTime();
    }

    protected void UpdateModificationDate() => UpdatedAt = UpdatedAt.GetCurrentUtcTime();
}
