using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Students;
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
}