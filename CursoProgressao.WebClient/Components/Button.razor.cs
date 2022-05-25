using Microsoft.AspNetCore.Components;

namespace CursoProgressao.WebClient.Components;

public partial class Button
{
    [Parameter] public string Text { get; set; } = "";
    [Parameter] public Action? OnClick { get; set; }
}
