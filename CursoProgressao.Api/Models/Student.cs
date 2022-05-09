namespace Api.Models
{
    public class Student : Model
    {
        public string Name { get; private set; }

        public Student(string name) : base()
        {
            Name = name;
        }
    }
}
