using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Shared.Dto.Documents;
using CursoProgressao.Shared.Dto.Responsibles;

namespace CursoProgressao.Server.Models;

public class Responsible : Model
{
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            UpdateModificationDate();
        }
    }
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            UpdateModificationDate();
        }
    }
    public ResponsibleDocument Document
    {
        get => _document;
        private set
        {
            _document = value;
            UpdateModificationDate();
        }
    }
    public IReadOnlyCollection<Student> Students => _students;

    private string _firstName;
    private string _lastName;
    private readonly List<Student> _students = new();
    private ResponsibleDocument _document = null!;

    public Responsible(string firstName, string lastName) : base()
    {
        _firstName = firstName;
        _lastName = lastName;
    }

    public async Task SetDocumentAsync(UpdateDocumentDto dto, Func<Guid, Task<bool>> checkIdUniquenessAsync)
    {
        if (Document is null)
        {
            if (dto.Rg is null || dto.Cpf is null) throw new BadRequestException("InvalidResponsibleDocument", "Responsible document must have a valid rg and cpf");

            ResponsibleDocument document = new(Id, dto.Rg, dto.Cpf);

            while (!await checkIdUniquenessAsync(document.Id)) document = new(Id, dto.Rg, dto.Cpf);

            Document = document;

            return;
        }

        if (dto.Rg is not null) Document.Rg = dto.Rg;
        if (dto.Cpf is not null) Document.Cpf = dto.Cpf;

        UpdateModificationDate();
    }

    public static implicit operator GetOneResponsibleDto?(Responsible? responsible)
        => responsible is not null ? new()
        {
            FirstName = responsible.FirstName,
            LastName = responsible.LastName,
            Document = responsible.Document
        } : null;
}
