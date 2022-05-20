using CursoProgressao.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Payments;

public class UpdatePaymentDto : IValidatableObject
{
    [Price]
    public float? Value { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? ReferenceDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Value is null && PaymentDate is null && ReferenceDate is null)
            yield return new ValidationResult("NoPaymentDataSent");
    }
}
