using CursoProgressao.Api.Exceptions.Base;

namespace CursoProgressao.Api.Exceptions.Documents;

public class NotUniqueRgException : ConflictException
{
    public NotUniqueRgException() : base("RgAlreadyExists") { }
}
