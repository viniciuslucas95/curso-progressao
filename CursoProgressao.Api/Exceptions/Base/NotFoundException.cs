using System.Net;

namespace CursoProgressao.Api.Exceptions.Base
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string? message) : base((int)HttpStatusCode.NotFound, message) { }
    }
}
