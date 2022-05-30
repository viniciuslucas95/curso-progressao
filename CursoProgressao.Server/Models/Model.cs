using CursoProgressao.Shared.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoProgressao.Server.Models;

public abstract class Model
{
    public Guid Id { get; private init; }
    [NotMapped]
    public DateTime CreatedAt { get; private init; }
    [NotMapped]
    public DateTime UpdatedAt { get; set; }

    public Model()
    {
        Id = Guid.NewGuid();
        CreatedAt = CreatedAt.GetCurrentUtcTime();
        UpdatedAt = UpdatedAt.GetCurrentUtcTime();
    }

    protected void UpdateModificationDate() => UpdatedAt = UpdatedAt.GetCurrentUtcTime();
}
