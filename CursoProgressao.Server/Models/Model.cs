using CursoProgressao.Server.Utils;

namespace CursoProgressao.Server.Models;

public abstract class Model
{
    public Guid Id { get; private init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }

    public Model()
    {
        Id = Guid.NewGuid();
        CreatedAt = CreatedAt.GetCurrentUtcTime();
        UpdatedAt = UpdatedAt.GetCurrentUtcTime();
    }

    public void UpdateModificationDate()
    {
        UpdatedAt = UpdatedAt.GetCurrentUtcTime();
    }
}
