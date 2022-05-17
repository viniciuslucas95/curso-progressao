using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Test.Attributes;
using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Responsibles;
using Xunit;

namespace CursoProgressao.Server.Test.Services;

[TestCaseOrderer("CursoProgressao.Server.Test.Attributes.PriorityOrderer", "CursoProgressao.Server.Test")]
public class ResponsiblesServiceTest : IClassFixture<ResponsiblesFixture>
{
    private readonly ResponsiblesFixture _fixture;

    public ResponsiblesServiceTest(ResponsiblesFixture fixture) => _fixture = fixture;

    [Fact, TestPriority(0)]
    public async Task ShouldCreateResponsible()
    {
        CreateResponsibleDto dto = new()
        {
            FirstName = "Carlos",
            LastName = "Daniel",
            Document = new()
            {
                Rg = "16.487.469-8",
                Cpf = "156.489.654-48"
            }
        };

        Guid id = await _fixture.Service.CreateAsync(dto);

        await _fixture.Context.SaveChangesAsync();

        _fixture.Id = id;

        Assert.True(id.ToString().Length > 0);
    }

    [Fact, TestPriority(1)]
    public async Task ShouldNotCreateResponsible()
    {
        CreateResponsibleDto dto = new()
        {
            FirstName = "Carlos",
            LastName = "Daniel",
            Document = new()
            {
                Rg = "16.487.469-8",
                Cpf = "156.489.654-48"
            }
        };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.Service.CreateAsync(dto));
    }

    [Fact, TestPriority(2)]
    public async Task ShouldUpdateResponsible()
    {
        UpdateResponsibleDto dto = new()
        {
            FirstName = "Pedro",
            Document = new()
            {
                Rg = "16.447.469-1"
            }
        };

        await _fixture.Service.UpdateAsync(_fixture.Id, dto);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, TestPriority(3)]
    public async Task ShouldNotUpdateResponsible()
    {
        UpdateResponsibleDto dto = new()
        {
            Document = new()
            {
                Rg = "16.447.469-1"
            }
        };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.Service.UpdateAsync(_fixture.Id, dto));
    }

    [Fact, TestPriority(4)]
    public async Task ShouldGetResponsible()
    {
        GetOneResponsibleDto result = await _fixture.Service.GetOneAsync(_fixture.Id);

        Assert.Equal("Pedro", result.FirstName);
        Assert.Equal("Daniel", result.LastName);
        Assert.Equal("16.447.469-1", result.Document.Rg);
        Assert.Equal("156.489.654-48", result.Document.Cpf);
    }

    [Fact, TestPriority(5)]
    public async Task ShouldNotGetResponsible()
        => await Assert.ThrowsAsync<BadRequestException>(() => _fixture.Service.GetOneAsync(Guid.NewGuid()));

    [Fact, TestPriority(6)]
    public async Task ShouldGetAllResponsibles()
    {
        IEnumerable<GetAllResponsiblesDto> results = await _fixture.Service.GetAllAsync();

        Assert.True(results.Any());
    }

    [Fact, TestPriority(7)]
    public async Task ShouldDeleteResponsible()
    {
        await _fixture.Service.DeleteAsync(_fixture.Id);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, TestPriority(8)]
    public async Task ShouldNotGetAllResponsibles()
    {
        IEnumerable<GetAllResponsiblesDto> results = await _fixture.Service.GetAllAsync();

        Assert.False(results.Any());
    }

    [Fact, TestPriority(9)]
    public async Task ShouldNotDeleteResponsible()
        => await Assert.ThrowsAsync<BadRequestException>(() => _fixture.Service.DeleteAsync(Guid.NewGuid()));
}
