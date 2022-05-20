using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Test.Attributes;
using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Students;
using Xunit;

namespace CursoProgressao.Server.Test.Services;

[TestCaseOrderer("CursoProgressao.Server.Test.Attributes.PriorityOrderer", "CursoProgressao.Server.Test")]
public class StudentsServiceTest : IClassFixture<StudentsFixture>
{
    private readonly StudentsFixture _fixture;

    public StudentsServiceTest(StudentsFixture fixture) => _fixture = fixture;

    [Fact, TestPriority(0)]
    public async Task ShouldCreateStudent()
    {
        CreateStudentDto dto = new()
        {
            FirstName = "Monkey",
            LastName = "D. Luffy",
            Note = "O cara que vai se tornar o rei dos piratas",
            Document = new()
            {
                Rg = "48.987.456-7",
                Cpf = "987.456.231-46"
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
            },
            BirthDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
        };

        Guid id = await _fixture.Service.CreateAsync(dto);

        await _fixture.Context.SaveChangesAsync();

        _fixture.Id = id;

        Assert.True(id.ToString().Length > 0);
    }

    [Fact, TestPriority(1)]
    public async Task ShouldNotCreateStudent()
    {
        CreateStudentDto dto = new()
        {
            FirstName = "Monkey",
            LastName = "D. Luffy",
            Note = "O cara que vai se tornar o rei dos piratas",
            Document = new()
            {
                Rg = "48.987.456-7",
                Cpf = "987.456.231-46"
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

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.Service.CreateAsync(dto));
    }

    [Fact, TestPriority(2)]
    public async Task ShouldUpdateStudent()
    {
        UpdateStudentDto dto = new()
        {
            FirstName = "Zoro",
            LastName = "Roronoa",
            Note = "World best swordman",
            Document = new()
            {
                Cpf = "438.654.789-49"
            },
            Contact = new()
            {
                Email = "roronoa.zoro@onepiece.com"
            }
        };

        await _fixture.Service.UpdateAsync(_fixture.Id, dto);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, TestPriority(3)]
    public async Task ShouldNotUpdateStudent()
    {
        UpdateStudentDto dto = new()
        {
            Document = new()
            {
                Cpf = "438.654.789-49"
            },
            Contact = new()
            {
                Email = "roronoa.zoro@onepiece.com"
            }
        };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.Service.UpdateAsync(_fixture.Id, dto));
    }

    //[Fact, TestPriority(4)]
    //public async Task ShouldGetStudent()
    //{
    //    GetOneStudentDto result = await _fixture.Service.GetOneAsync(_fixture.Id);

    //    Assert.Equal("Zoro", result.FirstName);
    //    Assert.Equal("Roronoa", result.LastName);
    //    Assert.Equal("World best swordman", result.Note);
    //    Assert.Equal("48.987.456-7", result.Document?.Rg);
    //    Assert.Equal("438.654.789-49", result.Document?.Cpf);
    //    Assert.Equal("(21) 94679-4679", result.Contact?.CellPhone);
    //    Assert.Equal("(11) 4567-4698", result.Contact?.Landline);
    //    Assert.Equal("Rua East Blue", result.Residence?.Address);
    //    Assert.Equal("16487-456", result.Residence?.ZipCode);
    //}

    //[Fact, TestPriority(5)]
    //public async Task ShouldNotGetStudent()
    //    => await Assert.ThrowsAsync<NotFoundException>(() => _fixture.Service.GetOneAsync(Guid.NewGuid()));

    [Fact, TestPriority(6)]
    public async Task ShouldGetAllStudents()
    {
        IEnumerable<GetAllStudentsDto> results = await _fixture.Service.GetAllAsync();

        Assert.True(results.Any());
    }

    [Fact, TestPriority(7)]
    public async Task ShouldDeleteStudent()
    {
        await _fixture.Service.DeleteAsync(_fixture.Id);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, TestPriority(8)]
    public async Task ShouldNotGetAllStudents()
    {
        IEnumerable<GetAllStudentsDto> results = await _fixture.Service.GetAllAsync();

        Assert.False(results.Any());
    }

    [Fact, TestPriority(9)]
    public async Task ShouldNotDeleteStudent()
        => await Assert.ThrowsAsync<NotFoundException>(() => _fixture.Service.DeleteAsync(Guid.NewGuid()));
}
