using CursoProgressao.Shared.Dto.Students;

namespace CursoProgressao.WebClient.Stores;

public class StudentsStore
{
    public Dictionary<Guid, GetAllStudentsDto> Students = new();
}
