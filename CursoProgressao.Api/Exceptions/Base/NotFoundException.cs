using System.Net;

namespace CursoProgressao.Api.Exceptions.Base
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string name, string message = "") : base((int)HttpStatusCode.NotFound, name, message) { }
    }
}
