using CursoProgressao.Api.Models.Documents;

namespace CursoProgressao.Api.Models;

public class Responsible : Model
{
    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            UpdateModificationDate();
        }
    }
    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            UpdateModificationDate();
        }
    }
    public Guid StudentId { get; private init; }
    public Student Student { get; private init; } = null!;
    public ResponsibleDocument Document { get; private init; }

    private string? _firstName = "";
    private string? _lastName = "";

    public Responsible(Guid studentId)
    {
        StudentId = studentId;
        Document = new(Id);
    }
}
