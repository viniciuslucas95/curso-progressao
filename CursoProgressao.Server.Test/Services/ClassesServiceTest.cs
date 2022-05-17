using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Test.Attributes;
using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Classes;
using Xunit;

namespace CursoProgressao.Server.Test.Services;

[TestCaseOrderer("CursoProgressao.Server.Test.Attributes.PriorityOrderer", "CursoProgressao.Server.Test")]
public class ClassesServiceTest : IClassFixture<ClassesFixture>
{
    private readonly ClassesFixture _fixture;

    public ClassesServiceTest(ClassesFixture fixture) => _fixture = fixture;

    [Fact, TestPriority(0)]
    public async Task ShouldCreateClass()
    {
        CreateClassDto dto = new() { Name = "EPCAR" };

        Guid id = await _fixture.Service.CreateAsync(dto);

        await _fixture.Context.SaveChangesAsync();

        _fixture.Id = id;

        Assert.True(id.ToString().Length > 0);
    }

    [Fact, TestPriority(1)]
    public async Task ShouldNotCreateClass()
    {
        CreateClassDto dto = new() { Name = "EPCAR" };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.Service.CreateAsync(dto));
    }

    [Fact, TestPriority(2)]
    public async Task ShouldUpdateClass()
    {
        UpdateClassDto dto = new() { Name = "EsPCEx" };

        await _fixture.Service.UpdateAsync(_fixture.Id, dto);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, TestPriority(3)]
    public async Task ShouldNotUpdateClass()
    {
        UpdateClassDto dto = new() { Name = "EsPCEx" };

        await Assert.ThrowsAsync<ConflictException>(() => _fixture.Service.UpdateAsync(_fixture.Id, dto));
    }

    [Fact, TestPriority(4)]
    public async Task ShouldGetClass()
    {
        GetOneClassDto result = await _fixture.Service.GetOneAsync(_fixture.Id);

        Assert.True(result.Name == "EsPCEx");
    }

    [Fact, TestPriority(5)]
    public async Task ShouldNotGetClass()
        => await Assert.ThrowsAsync<BadRequestException>(() => _fixture.Service.GetOneAsync(Guid.NewGuid()));

    [Fact, TestPriority(6)]
    public async Task ShouldGetAllClasses()
    {
        IEnumerable<GetAllClassesDto> results = await _fixture.Service.GetAllAsync();

        Assert.True(results.Any());
    }

    [Fact, TestPriority(7)]
    public async Task ShouldDeleteClass()
    {
        await _fixture.Service.DeleteAsync(_fixture.Id);

        await _fixture.Context.SaveChangesAsync();
    }

    [Fact, TestPriority(8)]
    public async Task ShouldNotGetAllClasses()
    {
        IEnumerable<GetAllClassesDto> results = await _fixture.Service.GetAllAsync();

        Assert.False(results.Any());
    }

    [Fact, TestPriority(9)]
    public async Task ShouldNotDeleteClass()
        => await Assert.ThrowsAsync<BadRequestException>(() => _fixture.Service.DeleteAsync(Guid.NewGuid()));
}
