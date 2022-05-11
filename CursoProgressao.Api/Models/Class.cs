namespace CursoProgressao.Api.Models;

public class Class : Model
{
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            UpdateModificationDate();
        }
    }
    public List<Student> Students { get; set; }

    private string _name;
    //private readonly List<Student> _students;

    public Class(string name)
    {
        Students = new List<Student>();
        _name = name;
    }
}
