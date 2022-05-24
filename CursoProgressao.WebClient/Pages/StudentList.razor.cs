using CursoProgressao.Shared.Constants;
using CursoProgressao.Shared.Dto.Students;
using static CursoProgressao.WebClient.Components.TextField;

namespace CursoProgressao.WebClient.Pages;

public partial class StudentList
{
    private readonly Dictionary<string, TextFieldData> _textFields;
    private readonly List<GetAllStudentsDto> _students = new()
    {

        new GetAllStudentsDto()
        {
            FirstName = "Carlos",
            LastName = "Daniel de Almeida",
            Contract = new()
            {
                Class = "EPCAR",
                IsOwing = false
            },
            Responsible = new()
            {
                FirstName = "Pedro",
                LastName = "Alvares de Almeida",
                Document = new()
                {
                    Rg = "48.879.251-4",
                    Cpf = "456.987.123-45"
                }
            },
            Contact = new()
            {
                Email = "carlos.daniel@hotmail.com",
                Landline = "(21) 2468-4879",
                CellPhone = "(21) 94689-1464"
            }
        },
        new GetAllStudentsDto()
        {
            FirstName = "Fátima",
            LastName = "Bernardes",
            Contract = new()
            {
                Class = "EspeCex",
                IsOwing = false
            },
            Document = new()
            {
                Rg = "12.123.123-2",
                Cpf = "416.564.874-46"
            },
            Responsible = new()
            {
                FirstName = "Ana",
                LastName = "Bernardes",
                Document = new()
                {
                    Rg = "18.89.251-1",
                    Cpf = "456.987.233-45"
                }
            }
        }, new GetAllStudentsDto()
        {
            FirstName = "William",
            LastName = "Bonner da Silva",
            Responsible = new()
            {
                FirstName = "João",
                LastName = "de Jesus",
                Document = new()
                {
                    Rg = "48.879.251-4",
                    Cpf = "456.987.123-45"
                }
            },Contact = new()
            {
                CellPhone="(21) 94687-4697",
                Landline="(21) 4679-1646",
                Email="joao.jesus.12313@gmail.com"
            }
        },
        new GetAllStudentsDto()
        {
            FirstName = "Luffy",
            LastName = "Monkey D.",
            Contract = new()
            {
                Class = "EPCAR",
                IsOwing = true
            },
            Responsible = new()
            {
                FirstName = "Pedro",
                LastName = "Paulo de Almeida",
                Document = new()
                {
                    Rg = "48.879.251-4",
                    Cpf = "456.987.123-45"
                }
            }
        },
        new GetAllStudentsDto()
        {
            FirstName = "Carlos",
            LastName = "Daniel",
            Contract = new()
            {
                Class = "EPCAR",
                IsOwing = true
            },
            Responsible = new()
            {
                FirstName = "Pedro",
                LastName = "Paulo de Almeida",
                Document = new()
                {
                    Rg = "48.879.251-4",
                    Cpf = "456.987.123-45"
                }
            }
        },
        new GetAllStudentsDto()
        {
            FirstName = "Carlos",
            LastName = "Daniel",
            Contract = new()
            {
                Class = "EPCAR",
                IsOwing = false
            },
            Document = new()
            {
                Rg = "12.123.123-2",
                Cpf = "456.564.874-46"
            },
            Responsible = new()
            {
                FirstName = "Pedro",
                LastName = "Paulo de Almeida",
                Document = new()
                {
                    Rg = "48.879.251-4",
                    Cpf = "456.987.123-45"
                }
            }
        }
    };

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
