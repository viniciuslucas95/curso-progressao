using CursoProgressao.Shared.Attributes;

namespace CursoProgressao.Shared.Dto.Contracts;

public class CreateContractDto
{
    [CustomRequired]
    public DateTime StartDate { get; set; }
    [CustomRequired]
    public DateTime EndDate { get; set; }
    [CustomRequired]
    [Price]
    public float PaymentValue { get; set; }
    [CustomRequired]
    [Day]
    public int DueDateDay { get; set; }
    [CustomRequired]
    public Guid ClassId { get; set; }
}
