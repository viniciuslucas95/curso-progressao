namespace CursoProgressao.Api.Exceptions.Base
{
    public abstract class BaseException : Exception
    {
        public int StatusCode { get; private init; }
        public string Name { get; set; }

        public BaseException(int statusCode, string name, string message = "") : base(message)
        {
            StatusCode = statusCode;
            Name = name;
        }
    }
}
