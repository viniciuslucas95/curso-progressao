using CursoProgressao.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Documents;

public class UpdateDocumentDto : IValidatableObject
{
    [CustomRegex("^[0-9]{2}.[0-9]{3}.[0-9]{3}-[0-9]{1}$", "XX.XXX.XXX-X")]
    public string? Rg { get; set; }
    [CustomRegex("^[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}$", "XXX.XXX.XXX-XX")]
    public string? Cpf { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Rg) &&
            string.IsNullOrEmpty(Cpf))
            yield return new ValidationResult("NoDocumentDataSent");
    }
}
