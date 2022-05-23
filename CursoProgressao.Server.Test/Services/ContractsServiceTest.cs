using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Contracts;
using Xunit;
using Xunit.Extensions.Ordering;

namespace CursoProgressao.Server.Test.Services;

[Collection("School"), Order(3)]
public class ContractsServiceTest
{
    private readonly SchoolDbFixture _fixture;

    public ContractsServiceTest(SchoolDbFixture fixture) => _fixture = fixture;

    [Fact, Order(0)]
    public async Task ShouldCreateContract()
    {
        DateTime now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        DateTime oneYearFromNow = now.AddYears(1);

        CreateContractDto dto = new()
        {
            ClassId = _fixture.ClassId,
            DueDateDay = 8,
            StartDate = now,
            EndDate = oneYearFromNow,
            PaymentValue = 650
        };

        _fixture.ContractId = await _fixture.ContractsService.CreateAsync(_fixture.StudentId, dto, _fixture.StudentsService.CheckExistenceAsync);

        await _fixture.Context.SaveChangesAsync();

        Assert.True(_fixture.ContractId.ToString().Length > 0);
    }
}
