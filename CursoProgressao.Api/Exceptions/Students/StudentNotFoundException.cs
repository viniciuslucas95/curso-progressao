using CursoProgressao.Api.Exceptions.Base;

namespace CursoProgressao.Api.Exceptions.Students
{
    public class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException() : base("StudentNotFound") { }
    }
}
