using CursoProgressao.Api.Exceptions.Base;

namespace CursoProgressao.Api.Exceptions.Common;

public class InvalidPropertyException : BadRequestException
{
    public InvalidPropertyException() : base("InvalidProperty") { }
}
