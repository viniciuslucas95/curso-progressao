using CursoProgressao.Api.Exceptions.Base;

namespace CursoProgressao.Api.Exceptions.Classes;

public class ClassNotFoundException : NotFoundException
{
    public ClassNotFoundException() : base("ClassNotFound") { }
}
