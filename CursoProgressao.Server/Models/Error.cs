using CursoProgressao.Shared.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoProgressao.Server.Models;

public class Error
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private init; }
    public string Name { get; private init; }
    public string Message { get; private init; }
    public DateTime CreatedAt { get; private init; }

    public Error(string name, string message) : base()
    {
        Name = name;
        Message = message;
        CreatedAt = CreatedAt.GetCurrentUtcTime();
    }
}
