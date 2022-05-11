using System.Net;

namespace CursoProgressao.Api.Exceptions.Base;

public class ConflictException : BaseException
{
    public ConflictException(string name, string message = "") : base((int)HttpStatusCode.Conflict, name, message) { }
}
