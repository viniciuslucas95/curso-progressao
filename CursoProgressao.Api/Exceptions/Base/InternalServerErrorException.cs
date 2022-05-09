using System.Net;

namespace Api.Exceptions.Base
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string? message) : base((int)HttpStatusCode.InternalServerError, message) { }
    }
}
