using System.Net;

namespace Api.Exceptions.Base
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string? message) : base((int)HttpStatusCode.NotFound, message) { }
    }
}
