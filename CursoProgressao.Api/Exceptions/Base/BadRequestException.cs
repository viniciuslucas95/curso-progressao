using System.Net;

namespace CursoProgressao.Api.Exceptions.Base
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string name, string message = "") : base((int)HttpStatusCode.BadRequest, name, message) { }
    }
}
