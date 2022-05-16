using Microsoft.AspNetCore.Components;

namespace CursoProgressao.WebClient.Components;

public partial class TextField
{
    [Parameter] public TextFieldData Data { get; set; } = new();
    [Parameter] public EventCallback<ChangeEventArgs> OnTextChangedCallback { get; set; }
    [Parameter] public Action<bool>? OnFocusCallback { get; set; }
    private bool IsFocused { get; set; } = false;

    private void OnFocus(bool isFocused)
    {
        IsFocused = isFocused;
        OnFocusCallback?.Invoke(isFocused);
    }

    public class TextFieldData
    {
        public string Label { get; set; } = "";
        public string Text { get; set; } = "";
        public string HelpText { get; set; } = "";
        public bool HasError { get; set; } = false;
    }
}
