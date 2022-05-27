using CursoProgressao.Shared.Dto.Students;

namespace CursoProgressao.WebClient.Repositories.Students;

public class FakeStudentsRepository : IStudentsRepository
{
    public async Task<IEnumerable<GetAllStudentsDto>> GetAllAsync()
    {
        List<GetAllStudentsDto> result = new()
        {
            new GetAllStudentsDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "Carlos",
                LastName = "Daniel de Almeida",
                Contract = new()
                {
                    ActiveClassesId = new[] {Guid.NewGuid()},
                    IsOwing = false
                },
                Responsible = new()
                {
                    FirstName = "Pedro",
                    LastName = "Alvares de Almeida",
                    Document = new()
                    {
                        Rg = "48.879.251-4",
                        Cpf = "456.987.123-45"
                    }
                },
                Contact = new()
                {
                    Email = "carlos.daniel@hotmail.com",
                    Landline = "(21) 2468-4879",
                    CellPhone = "(21) 94689-1464"
                }
            }
        };

        return await Task.FromResult(result);
    }
}
