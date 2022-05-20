using CursoProgressao.Shared.Dto.Documents;

namespace CursoProgressao.Server.Models;

public class ResponsibleDocument : Model
{
    public string? Rg
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
    public Guid ResponsibleId { get; private init; }
    public Responsible Responsible { get; private init; } = null!;

    private string? _rg;
    private string _cpf;

    public ResponsibleDocument(Guid responsibleId, string cpf, string? rg = null) : base()
    {
        ResponsibleId = responsibleId;
        _rg = rg;
        _cpf = cpf;
    }

    public static implicit operator GetOneDocumentDto(ResponsibleDocument document)
        => new()
        {
            Rg = document.Rg,
            Cpf = document.Cpf
        };
}
