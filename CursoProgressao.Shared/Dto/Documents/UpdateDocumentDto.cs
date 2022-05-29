using CursoProgressao.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Documents;

public class UpdateDocumentDto : IValidatableObject
{
    [ExactLength(9)]
    public string? Rg { get; set; }
    [ExactLength(11)]
    public string? Cpf { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Rg) &&
            string.IsNullOrEmpty(Cpf))
            yield return new ValidationResult("NoDocumentDataSent");
    }
}
