using CursoProgressao.Api.Attributes;

namespace CursoProgressao.Api.Dto.Residences
{
    public class ModifyResidenceDto
    {
        [CustomRegex("^[0-9]{5}-[0-9]{3}$", "XXXXX-XXX")]
        public string? ZipCode { get; set; }
        [CustomMinLengthAttribute(3)]
        public string? Address { get; set; }
    }
}
