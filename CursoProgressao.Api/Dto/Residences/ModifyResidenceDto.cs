using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Residences
{
    public class ModifyResidenceDto
    {
        [RegularExpression("^[0-9]{5}-[0-9]{3}$", ErrorMessage = "Zip code format must be XXXXX-XXX")]
        public string? ZipCode { get; set; }
        [MinLength(3, ErrorMessage = "Address must have at least 3 characters")]
        public string? Address { get; set; }
    }
}
