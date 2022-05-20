using CursoProgressao.Shared.Attributes;

namespace CursoProgressao.Shared.Dto.Payments;

public class CreatePaymentDto
{
    [CustomRequired]
    [Price]
    public float Value { get; set; }
    [CustomRequired]
    public DateTime PaymentDate { get; set; }
    [CustomRequired]
    public DateTime ReferenceDate { get; set; }
}
