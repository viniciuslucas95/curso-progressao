namespace CursoProgressao.Api.Models.Documents;

public class StudentDocument : Document
{
    public Guid StudentId { get; private init; }
    public Student Student { get; private init; } = null!;

    public StudentDocument(Guid studentId)
    {
        StudentId = studentId;
    }
}
