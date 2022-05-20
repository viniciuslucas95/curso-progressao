using CursoProgressao.Shared.Dto.Residences;

namespace CursoProgressao.Server.Models;

public class Residence : Model
{
    public string? ZipCode
    {
        get => _zipCode;
        set
        {
            _zipCode = value;
            UpdateModificationDate();
        }
    }
    public string? Address
    {
        get => _address;
        set
        {
            _address = value;
            UpdateModificationDate();
        }
    }
    public Guid StudentId { get; private init; }
    public Student Student { get; private init; } = null!;

    private string? _zipCode;
    private string? _address;

    public Residence(Guid studentId, string? zipCode, string? address) : base()
    {
        StudentId = studentId;
        _zipCode = zipCode;
        _address = address;
    }

    public static implicit operator GetOneResidenceDto?(Residence? residence)
        => residence is not null ? new()
        {
            Address = residence.Address,
            ZipCode = residence.ZipCode
        } : null;
}
