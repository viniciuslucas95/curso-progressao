using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Contracts;

public class UpdateContractDto : IValidatableObject
{
    public float? PaymentValue { get; set; }
    public int? DueDateDay { get; set; }
    public DateTime? CancelDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PaymentValue is null && DueDateDay is null && CancelDate is null)
            yield return new ValidationResult("NoContractDataSent");
    }
}
