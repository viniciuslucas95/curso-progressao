namespace CursoProgressao.Api.Dto.Errors;

public class ErrorDto<T> where T : ErrorItemDto
{
    public List<T> Errors { get; set; } = new();
}
