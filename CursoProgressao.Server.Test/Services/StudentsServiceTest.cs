using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Students;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Extensions.Ordering;

namespace CursoProgressao.Server.Test.Services;

[Collection("School"), Order(2)]
public class StudentsServiceTest
{
    private readonly SchoolDbFixture _fixture;

    public StudentsServiceTest(SchoolDbFixture fixture) => _fixture = fixture;

    [Fact, Order(0)]
    public async Task ShouldCreateStudent()
    {
        CreateStudentDto dto = new()
        {
            FirstName = "Monkey",
            LastName = "D. Luffy",
            Note = "The guy who will become the king of the pirates",
            BirthDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            ResponsibleId = _fixture.ResponsibleId,
            Document = new()
            {
                Rg = "18.548.649-4",
                Cpf = "187.465.487-45"
            },
            Contact = new()
            {
                CellPhone = "(21) 94679-4679",
                Landline = "(11) 4567-4698",
                Email = "monkey.d.luffy@onepiece.com"
            },
            Residence = new()
            {
                Address = "Rua East Blue",
                ZipCode = "16487-456"
            }
        };

        _fixture.StudentId = await _fixture.StudentsService.CreateAsync(dto);

        await _fixture.Context.SaveChangesAsync();

        Assert.True(_fixture.StudentId.ToString().Length > 0);
    }

    [Fact, Order(1)]
    public void ShouldNotCreateStudentBecauseOfLackOfInfoProvided()
    {
        CreateStudentDto dto = new()
        {
            FirstName = "Monkey",
            LastName = "D. Luffy",
            Note = "The guy who will become the king of the pirates",
            BirthDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            Document = new()
            {
                Rg = "18.548.649-1"
            },
            Contact = new()
            {
                CellPhone = "(21) 94679-4679",
                Landline = "(11) 4567-4698",
                Email = "monkey.d.luffy.wrong@onepiece.com"
            },
            Residence = new()
            {
                Address = "Rua East Blue",
                ZipCode = "16487-456"
            }
        };

        ValidationContext context = new(dto);
        List<ValidationResult> results = new();
        bool isValid = Validator.TryValidateObject(dto, context, results, true);
        string expected = "StudentWithoutInfo!--!Student cpf or responsible must be provided";
        string? actual = results[0].ErrorMessage;

        Assert.False(isValid);
        Assert.Equal(expected, actual);
    }

    [Fact, Order(1)]
    public void ShouldNotCreateStudentBecauseOfEmptyNames()
    {
        CreateStudentDto dto = new()
        {
            FirstName = "",
            LastName = "",
            Document = new()
            {
                Rg = "48.548.649-1",
                Cpf = "123.123.123-12"
            }
        };

        ValidationContext context = new(dto);
        List<ValidationResult> results = new();
        bool isValid = Validator.TryValidateObject(dto, context, results, true);
        string expectedFirstName = "RequiredFirstName!--!First name cannot be empty";
        string? actualFirstName = results[0].ErrorMessage;
        string expectedLastName = "RequiredLastName!--!Last name cannot be empty";
        string? actualLastName = results[1].ErrorMessage;

        Assert.False(isValid);
        Assert.Equal(expectedFirstName, actualFirstName);
        Assert.Equal(expectedLastName, actualLastName);
    }

    [Fact, Order(1)]
    public void ShouldNotCreateStudentBecauseOfTooShortNames()
    {
        CreateStudentDto dto = new()
        {
            FirstName = "A",
            LastName = "B",
            Document = new()
            {
                Rg = "48.548.649-1",
                Cpf = "123.123.123-12"
            }
        };

        ValidationContext context = new(dto);
        List<ValidationResult> results = new();
        bool isValid = Validator.TryValidateObject(dto, context, results, true);
        string expectedFirstName = "FirstNameTooShort!--!First name must have at least 2 characters";
        string? actualFirstName = results[0].ErrorMessage;
        string expectedLastName = "LastNameTooShort!--!Last name must have at least 2 characters";
        string? actualLastName = results[1].ErrorMessage;

        Assert.False(isValid);
        Assert.Equal(expectedFirstName, actualFirstName);
        Assert.Equal(expectedLastName, actualLastName);
    }
}