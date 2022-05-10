namespace CursoProgressao.Api.Dto.Residences
{
    public class GetOneResidenceDto
    {
        public uint? ZipCode { get; set; }
        public string? Address { get; set; }
        public ulong? Landline { get; set; }
        public ulong? CellPhone { get; set; }
    }
}
