using CursoProgressao.Server.Dto.Documents;

namespace CursoProgressao.Server.Models;

public class StudentDocument : Model
{
    public string Rg
    {
        get => _rg;
        set
        {
            _rg = value;
            UpdateModificationDate();
        }
    }
    public string Cpf
    {
        get => _cpf;
        set
        {
            _cpf = value;
            UpdateModificationDate();
        }
    }
    public Guid StudentId { get; private init; }
    public Student Student { get; private init; } = null!;

    private string _rg;
    private string _cpf;

    public StudentDocument(Guid studentId, string rg, string cpf) : base()
    {
        StudentId = studentId;
        _rg = rg;
        _cpf = cpf;
    }

    public static implicit operator GetOneDocumentDto?(StudentDocument? document)
        => document is not null ? new()
        {
            Rg = document.Rg,
            Cpf = document.Cpf
        } : null;
}
