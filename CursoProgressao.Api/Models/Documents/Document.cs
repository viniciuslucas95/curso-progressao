using CursoProgressao.Api.Dto.Documents;

namespace CursoProgressao.Api.Models.Documents;

public abstract class Document : Model
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
    public string? Cpf
    {
        get => _cpf;
        set
        {
            _cpf = value;
            UpdateModificationDate();
        }
    }

    private string? _rg;
    private string? _cpf;

    public static implicit operator GetOneDocumentDto(Document document)
        => new()
        {
            Rg = document.Rg,
            Cpf = document.Cpf
        };
}
