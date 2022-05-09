using System.Net;

namespace Api.Exceptions.Base
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string? message) : base((int)HttpStatusCode.BadRequest, message) { }
    }
}
