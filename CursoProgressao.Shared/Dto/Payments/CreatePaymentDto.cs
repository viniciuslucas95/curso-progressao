using CursoProgressao.Shared.Attributes;

namespace CursoProgressao.Shared.Dto.Payments;

public class CreatePaymentDto
{
    [Required]
    [Price]
    public float Value { get; set; }
    [Required]
    public DateTime PaymentDate { get; set; }
    [Required]
    public DateTime ReferenceDate { get; set; }
}
