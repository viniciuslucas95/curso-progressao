using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Classes;
using Xunit;
using Xunit.Extensions.Ordering;

namespace CursoProgressao.Server.Test.Services;

[Collection("School"), Order(0)]
public class ClassesServiceTest
{
    private readonly SchoolDbFixture _fixture;

    public ClassesServiceTest(SchoolDbFixture fixture) => _fixture = fixture;

    [Fact, Order(0)]
    public async Task ShouldCreateClass()
    {
        CreateClassDto dto = new() { Name = "EPCAR" };

        _fixture.ClassId = await _fixture.ClassesService.CreateAsync(dto);

        await _fixture.Context.SaveChangesAsync();

        Assert.True(_fixture.ClassId.ToString().Length > 0);
    }

    [Fact, Order(1)]
    public async Task ShouldNotCreateClassBecauseOfConflictName()
    {
        CreateClassDto dto = new() { Name = "EPCAR" };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.ClassesService.CreateAsync(dto));
    }

    [Fact, Order(2)]
    public async Task ShouldUpdateClass()
    {
        UpdateClassDto dto = new() { Name = "EsPCEx" };

        await _fixture.ClassesService.UpdateAsync(_fixture.ClassId, dto);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, Order(3)]
    public async Task ShouldNotUpdateClassBecauseOfConflictName()
    {
        UpdateClassDto dto = new() { Name = "EsPCEx" };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.ClassesService.UpdateAsync(_fixture.ClassId, dto));
    }

    [Fact, Order(4)]
    public async Task ShouldGetClass()
    {
        GetOneClassDto result = await _fixture.ClassesService.GetOneAsync(_fixture.ClassId);

        Assert.True(result.Name == "EsPCEx");
    }

    [Fact, Order(5)]
    public async Task ShouldNotGetClassBecauseOfWrongId()
        => await Assert.ThrowsAsync<NotFoundException>(() => _fixture.ClassesService.GetOneAsync(Guid.Empty));

    [Fact, Order(6)]
    public async Task ShouldGetAllClasses()
    {
        IEnumerable<GetAllClassesDto> results = await _fixture.ClassesService.GetAllAsync();

        Assert.True(results.Any());
    }

    [Fact, Order(7)]
    public async Task ShouldDeleteClass()
    {
        await _fixture.ClassesService.DeleteAsync(_fixture.ClassId);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, Order(8)]
    public async Task ShouldNotGetAllClassesBecauseOfEmptyList()
    {
        IEnumerable<GetAllClassesDto> results = await _fixture.ClassesService.GetAllAsync();

        Assert.False(results.Any());
    }

    [Fact, Order(9)]
    public async Task ShouldNotDeleteClassBecauseOfWrongId()
        => await Assert.ThrowsAsync<NotFoundException>(() => _fixture.ClassesService.DeleteAsync(Guid.Empty));

    [Fact, Order(10)]
    public async Task ShouldCreateAnotherClassForFurtherTests()
    {
        CreateClassDto dto = new() { Name = "EPCAR" };

        _fixture.ClassId = await _fixture.ClassesService.CreateAsync(dto);

        await _fixture.Context.SaveChangesAsync();

        Assert.True(_fixture.ClassId.ToString().Length > 0);
    }
}
