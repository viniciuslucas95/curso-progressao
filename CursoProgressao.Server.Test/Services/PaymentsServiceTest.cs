using CursoProgressao.Server.Test.Fixtures;
using CursoProgressao.Shared.Dto.Payments;
using Xunit;
using Xunit.Extensions.Ordering;

namespace CursoProgressao.Server.Test.Services;

[Collection("School"), Order(4)]
public class PaymentsServiceTest
{
    private readonly SchoolDbFixture _fixture;

    public PaymentsServiceTest(SchoolDbFixture fixture) => _fixture = fixture;

    [Fact, Order(0)]
    public async Task ShouldCreatePayment()
    {
        DateTime now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        CreatePaymentDto dto = new()
        {
            PaymentDate = now,
            ReferenceDate = now,
            Value = 650
        };

        _fixture.PaymentId = await _fixture.PaymentsService.CreateAsync(_fixture.ContractId, dto);

        await _fixture.Context.SaveChangesAsync();

        Assert.True(_fixture.ContractId.ToString().Length > 0);
    }
}
