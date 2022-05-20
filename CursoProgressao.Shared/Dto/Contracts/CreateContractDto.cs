using CursoProgressao.Shared.Attributes;
using CursoProgressao.Shared.Dto.Payments;

namespace CursoProgressao.Shared.Dto.Contracts;

public class CreateContractDto
{
    [CustomRequired]
    public DateTime StartDate { get; set; }
    [CustomRequired]
    public DateTime EndDate { get; set; }
    [CustomRequired]
    public float PaymentValue { get; set; }
    [CustomRequired]
    public int DueDateDay { get; set; }
    [CustomRequired]
    public Guid ClassId { get; set; }
    public CreatePaymentDto? Payment { get; set; }
}
