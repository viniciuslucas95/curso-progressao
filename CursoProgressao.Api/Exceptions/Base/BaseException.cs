namespace CursoProgressao.Api.Exceptions.Base
{
    public abstract class BaseException : Exception
    {
        public int StatusCode { get; private init; }

        public BaseException(int statusCode, string? message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
