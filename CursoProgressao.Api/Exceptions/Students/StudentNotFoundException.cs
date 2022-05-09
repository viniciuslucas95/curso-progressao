using Api.Exceptions.Base;

namespace Api.Exceptions.Students
{
    public class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException() : base("StudentNotFound") { }
    }
}
