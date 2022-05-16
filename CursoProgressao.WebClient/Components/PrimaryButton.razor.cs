using Microsoft.AspNetCore.Components;

namespace CursoProgressao.WebClient.Components;

public partial class PrimaryButton
{
    [Parameter]
    public string Text { get; set; } = "";
}
