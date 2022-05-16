using System.Net;

namespace CursoProgressao.Server.Exceptions.Base;

public class InternalServerErrorException : BaseException
{
    public InternalServerErrorException(string name, string message = "") : base((int)HttpStatusCode.InternalServerError, name, message) { }
}
