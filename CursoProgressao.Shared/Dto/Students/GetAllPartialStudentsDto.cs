namespace CursoProgressao.Shared.Dto.Students;

public class GetAllPartialStudentsDto
{
    public int Count { get; set; }
    public IEnumerable<GetPartialStudentDto> Students { get; set; } = null!;
}
