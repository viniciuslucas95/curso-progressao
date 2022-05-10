namespace CursoProgressao.Api.Models.Documents;

public class ResponsibleDocument : Document
{
    public Guid ResponsibleId { get; private init; }
    public Responsible Responsible { get; private init; } = null!;

    public ResponsibleDocument(Guid responsibleId)
    {
        ResponsibleId = responsibleId;
    }
}
