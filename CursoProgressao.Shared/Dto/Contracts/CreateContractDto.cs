using CursoProgressao.Shared.Attributes;

namespace CursoProgressao.Shared.Dto.Contracts;

public class CreateContractDto
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    [Price]
    public float PaymentValue { get; set; }
    [Required]
    [Day]
    public int DueDateDay { get; set; }
    [Required]
    public Guid ClassId { get; set; }
}
