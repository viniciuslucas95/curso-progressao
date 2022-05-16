using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using static CursoProgressao.WebClient.Components.TextField;

namespace CursoProgressao.WebClient.Pages;

public partial class StudentList
{
    private TextFieldData FirstNameData { get; set; } = new()
    {
        Label = "Nome",
        HelpText = "Obrigatório"
    };

    private void OnFirstNameFocus(bool isFocused)
    {
        if (!isFocused) CheckFirstNameData();
        else ResetFirstNameHelpText();
    }

    private void OnFirstNameChanged(ChangeEventArgs args)
    {
        if (args.Value is null) return;

        string value = (string)args.Value;

        FirstNameData.Text = value;
    }

    private void ResetFirstNameHelpText()
    {
        FirstNameData.HelpText = "Obrigatório";
        FirstNameData.HasError = false;
    }

    private void CheckFirstNameData()
    {
        // FIX: IT IS BROKEN WITH MULTIPLE SPACES
        string pattern = "^[a-zA-ZáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛãõÃÕçÇ']+$|^[a-zA-ZáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛãõÃÕçÇ']+(?:\\s[a-zA-ZáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛãõÃÕçÇ']*)*[a-zA-ZáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛãõÃÕçÇ']+$|^[a-zA-ZáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛãõÃÕçÇ']+\\s[a-zA-ZáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛãõÃÕçÇ']+$";

        Match match = Regex.Match(FirstNameData.Text, pattern);

        if (match.Success)
        {
            ResetFirstNameHelpText();
            return;
        }

        FirstNameData.HelpText = "Nome inválido";
        FirstNameData.HasError = true;
    }
}
