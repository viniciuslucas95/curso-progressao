using CursoProgressao.Shared.Dto.Documents;
using CursoProgressao.Shared.Dto.Students;

namespace CursoProgressao.WebClient.Repositories.Students;

public class FakeStudentsRepository : IStudentsRepository
{
    public async Task<GetAllPartialStudentsDto> GetAllAsync(GetAllStudentsQueryDto? query = null)
    {
        IEnumerable<GetPartialStudentDto> result = new List<GetPartialStudentDto>();

        await Task.Run(() => result = new List<GetPartialStudentDto>()
        {
            new GetPartialStudentDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "Carlos",
                LastName = "Daniel",
                Document = new GetOneDocumentDto()
                {
                    Rg = "29.648.469-4",
                    Cpf = "123.654.789-46"
                },

            }
        });

        return new GetAllPartialStudentsDto()
        {
            Count = 0,
            Students = result
        };
    }
}
