using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Residences
{
    public class CreateResidenceDto : IValidatableObject
    {
        [RegularExpression("/^[0-9]{5}-[0-9]{3}$/s", ErrorMessage = "Zip code format must be XXXXX-XXX")]
        public uint? ZipCode { get; set; }
        public string? Address { get; set; }
        [RegularExpression("/^[(][0-9]{2}[)] [0-9]{4}-[0-9]{4}$/s", ErrorMessage = "Landline format must be (XX) XXXX-XXX")]
        public ulong? Landline { get; set; }
        [RegularExpression("/^[(][0-9]{2}[)] [0-9]{5}-[0-9]{4}$/s", ErrorMessage = "Cell phone format must be (XX) XXXXX-XXX")]
        public ulong? CellPhone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Address) && ZipCode is null && Landline is null && CellPhone is null)
            {
                yield return new ValidationResult("Residence must have either zip code, address, landline or cell phone");
            }
        }
    }
}
