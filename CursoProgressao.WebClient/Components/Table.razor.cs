using CursoProgressao.Shared.Dto.Students;
using Microsoft.AspNetCore.Components;

namespace CursoProgressao.WebClient.Components;

public partial class Table
{
    [Parameter] public GetAllStudentsDto[] Data { get; set; } = null!;
}
