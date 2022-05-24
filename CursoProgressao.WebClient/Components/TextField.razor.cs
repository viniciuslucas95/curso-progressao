using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace CursoProgressao.WebClient.Components;

public partial class TextField
{
    [Parameter] public TextFieldData Data { get; set; } = new();

    private bool _isFocused;

    private void OnFocus(bool isFocused)
    {
        _isFocused = isFocused;

        if (!isFocused) CheckError();
        else Data.OnErrorChanged?.Invoke(false, Data.TextFieldName);
    }

    private void OnInputChanged(ChangeEventArgs args)
    {
        if (args.Value is null) return;

        string value = (string)args.Value;

        Data.OnTextChanged?.Invoke(value, Data.TextFieldName);
    }

    private void CheckError()
    {
        Match match = Regex.Match(Data.Text, Data.Pattern);

        if (match.Success)
        {
            Data.OnErrorChanged?.Invoke(false, Data.TextFieldName);
            return;
        }

        else Data.OnErrorChanged?.Invoke(true, Data.TextFieldName);
    }

    public class TextFieldData
    {
        public string Text { get; set; } = "";
        public string CurrentHelpText { get; set; } = "";
        public bool HasError { get; set; }
        public string Label { get; init; } = "";
        public string HelpText { get; init; } = "";
        public string ErrorText { get; init; } = "";
        public string Pattern { get; init; } = "";
        public string TextFieldName { get; init; } = "";
        public Action<string, string> OnTextChanged { get; init; } = null!;
        public Action<bool, string> OnErrorChanged { get; init; } = null!;
    }
}
