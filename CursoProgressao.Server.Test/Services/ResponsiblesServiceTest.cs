using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Responsibles;
using Xunit;
using Xunit.Extensions.Ordering;

namespace CursoProgressao.Server.Test.Services;

[Collection("School"), Order(1)]
public class ResponsiblesServiceTest
{
    private readonly SchoolDbFixture _fixture;

    public ResponsiblesServiceTest(SchoolDbFixture fixture) => _fixture = fixture;

    [Fact, Order(0)]
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

        _fixture.ResponsibleId = await _fixture.ResponsiblesService.CreateAsync(dto);

        await _fixture.Context.SaveChangesAsync();

        Assert.True(_fixture.ResponsibleId.ToString().Length > 0);
    }

    [Fact, Order(1)]
    public async Task ShouldNotCreateResponsibleBecauseOfConflictRg()
    {
        CreateResponsibleDto dto = new()
        {
            FirstName = "Carlos",
            LastName = "Daniel",
            Document = new()
            {
                Rg = "16.487.469-8",
                Cpf = "156.489.654-28"
            }
        };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.ResponsiblesService.CreateAsync(dto));
    }

    [Fact, Order(1)]
    public async Task ShouldNotCreateResponsibleBecauseOfConflictCpf()
    {
        CreateResponsibleDto dto = new()
        {
            FirstName = "Carlos",
            LastName = "Daniel",
            Document = new()
            {
                Rg = "16.487.469-6",
                Cpf = "156.489.654-48"
            }
        };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.ResponsiblesService.CreateAsync(dto));
    }

    [Fact, Order(2)]
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

        await _fixture.ResponsiblesService.UpdateAsync(_fixture.ResponsibleId, dto);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, Order(3)]
    public async Task ShouldNotUpdateResponsibleBecauseOfConflictRg()
    {
        UpdateResponsibleDto dto = new()
        {
            Document = new()
            {
                Rg = "16.447.469-1"
            }
        };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.ResponsiblesService.UpdateAsync(_fixture.ResponsibleId, dto));
    }

    [Fact, Order(3)]
    public async Task ShouldNotUpdateResponsibleBecauseOfConflictCpf()
    {
        UpdateResponsibleDto dto = new()
        {
            Document = new()
            {
                Cpf = "156.489.654-48"
            }
        };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.ResponsiblesService.UpdateAsync(_fixture.ResponsibleId, dto));
    }

    [Fact, Order(4)]
    public async Task ShouldGetResponsible()
    {
        GetOneResponsibleDto result = await _fixture.ResponsiblesService.GetOneAsync(_fixture.ResponsibleId);

        Assert.Equal("Pedro", result.FirstName);
        Assert.Equal("Daniel", result.LastName);
        Assert.Equal("16.447.469-1", result.Document.Rg);
        Assert.Equal("156.489.654-48", result.Document.Cpf);
    }

    [Fact, Order(5)]
    public async Task ShouldNotGetResponsibleBecauseOfWrongId()
        => await Assert.ThrowsAsync<NotFoundException>(() => _fixture.ResponsiblesService.GetOneAsync(Guid.Empty));

    [Fact, Order(6)]
    public async Task ShouldGetAllResponsibles()
    {
        IEnumerable<GetAllResponsiblesDto> results = await _fixture.ResponsiblesService.GetAllAsync();

        Assert.True(results.Any());
    }

    [Fact, Order(7)]
    public async Task ShouldDeleteResponsible()
    {
        await _fixture.ResponsiblesService.DeleteAsync(_fixture.ResponsibleId);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, Order(8)]
    public async Task ShouldNotGetAllResponsiblesBecauseOfEmptyList()
    {
        IEnumerable<GetAllResponsiblesDto> results = await _fixture.ResponsiblesService.GetAllAsync();

        Assert.False(results.Any());
    }

    [Fact, Order(9)]
    public async Task ShouldNotDeleteResponsibleBecauseOfWrongId()
        => await Assert.ThrowsAsync<NotFoundException>(() => _fixture.ResponsiblesService.DeleteAsync(Guid.Empty));

    [Fact, Order(10)]
    public async Task ShouldCreateAnotherResponsibleForFurtherTests()
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

        _fixture.ResponsibleId = await _fixture.ResponsiblesService.CreateAsync(dto);

        await _fixture.Context.SaveChangesAsync();

        Assert.True(_fixture.ResponsibleId.ToString().Length > 0);
    }
}
