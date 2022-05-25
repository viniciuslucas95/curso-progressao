using CursoProgressao.Shared.Constants;
using CursoProgressao.Shared.Dto.Students;
using CursoProgressao.WebClient.Services.Students;
using CursoProgressao.WebClient.Stores;
using Microsoft.AspNetCore.Components;
using static CursoProgressao.WebClient.Components.TextField;

namespace CursoProgressao.WebClient.Pages;

public partial class StudentList
{
    [Inject] public IStudentsService StudentsService { get; set; } = null!;
    [Inject] public StudentsStore StudentsStore { get; set; } = null!;

    private readonly Dictionary<string, TextFieldData> _textFields;

    public StudentList()
    {
        base.OnInitialized();

        _textFields = new()
        {
            {
                "FirstName",
                new()
                {
                    Label = "Nome",
                    ErrorText = "Nome inválido",
                    TextFieldName = "FirstName",
                    Pattern = RegexPattern.NAME,
                    OnTextChanged = OnTextChanged,
                    OnErrorChanged = OnErrorChanged
                }
            },
            {
                "LastName",
                new()
                {
                    Label = "Sobrenome",
                    ErrorText = "Sobrenome inválido",
                    TextFieldName = "LastName",
                    Pattern = RegexPattern.NAME,
                    OnTextChanged = OnTextChanged,
                    OnErrorChanged = OnErrorChanged
                }
            },
            {
                "Rg",
                new()
                {
                    Label = "RG",
                    HelpText = "Ex. 99.999.999-9",
                    ErrorText = "Apenas 9 dígitos",
                    TextFieldName = "Rg",
                    CurrentHelpText = "Ex: 99.999.999-9",
                    Pattern = RegexPattern.RG,
                    OnTextChanged = OnTextChanged,
                    OnErrorChanged = OnErrorChanged
                }
            },
            {
                "Cpf",
                new()
                {
                    Label = "CPF",
                    HelpText = "Ex. 999.999.999-99",
                    ErrorText = "Apenas 11 dígitos",
                    TextFieldName = "Cpf",
                    CurrentHelpText = "Ex: 999.999.999-99",
                    Pattern = RegexPattern.CPF,
                    OnTextChanged = OnTextChanged,
                    OnErrorChanged = OnErrorChanged
                }
            },
            {
                "ResponsibleFirstName",
                new()
                {
                    Label = "Nome do Responsável",
                    ErrorText = "Nome inválido",
                    TextFieldName = "ResponsibleFirstName",
                    Pattern = RegexPattern.NAME,
                    OnTextChanged = OnTextChanged,
                    OnErrorChanged = OnErrorChanged
                }
            },
            {
                "ResponsibleLastName",
                new()
                {
                    Label = "Sobrenome do Responsável",
                    ErrorText = "Sobrenome inválido",
                    TextFieldName = "ResponsibleLastName",
                    Pattern = RegexPattern.NAME,
                    OnTextChanged = OnTextChanged,
                    OnErrorChanged = OnErrorChanged
                }
            },
            {
                "ResponsibleRg",
                new()
                {
                    Label = "RG do Responsável",
                    HelpText = "Ex. 99.999.999-9",
                    ErrorText = "Apenas 9 dígitos",
                    TextFieldName = "ResponsibleRg",
                    CurrentHelpText = "Ex: 99.999.999-9",
                    Pattern = RegexPattern.RG,
                    OnTextChanged = OnTextChanged,
                    OnErrorChanged = OnErrorChanged
                }
            },
            {
                "ResponsibleCpf",
                new()
                {
                    Label = "CPF do Responsável",
                    HelpText = "Ex. 999.999.999-99",
                    ErrorText = "Apenas 11 dígitos",
                    TextFieldName = "ResponsibleCpf",
                    CurrentHelpText = "Ex: 999.999.999-99",
                    Pattern = RegexPattern.CPF,
                    OnTextChanged = OnTextChanged,
                    OnErrorChanged = OnErrorChanged
                }
            }
        };
    }

    private async Task GetAllAsync()
    {
        IEnumerable<GetAllStudentsDto> result = await StudentsService.GetAllAsync();

        foreach (GetAllStudentsDto student in result)
        {
            StudentsStore.Students.Add(student.Id, student);
        }

        StateHasChanged();
    }

    private void OnTextChanged(string text, string textFieldName)
        => _textFields[textFieldName].Text = text;

    private void OnErrorChanged(bool error, string textFieldName)
    {
        _textFields[textFieldName].HasError = error;

        _textFields[textFieldName].CurrentHelpText = error ?
            _textFields[textFieldName].ErrorText :
            _textFields[textFieldName].HelpText;
    }
}
