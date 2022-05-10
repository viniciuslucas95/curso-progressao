using CursoProgressao.Api.Dto.Residences;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Models
{
    public class Residence : Model
    {
        [Display(Name = "Zip Code")]
        [RegularExpression("^[0-9]{5}-[0-9]{3}$", ErrorMessage = "Zip code format must be XXXXX-XXX")]
        public uint? ZipCode { get; private set; }
        public string? Address { get; private set; }

        public Residence(uint? zipCode, string? address)
        {
            ZipCode = zipCode;
            Address = address;

        }

        public static implicit operator GetOneResidenceDto(Residence residence)
            => new()
            {
                Address = residence.Address,
                ZipCode = residence.ZipCode
            };
    }
}
