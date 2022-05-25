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
                    Class = "EPCAR",
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
            },
            new GetAllStudentsDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "Fátima",
                LastName = "Bernardes",
                Contract = new()
                {
                    Class = "EspeCex",
                    IsOwing = false
                },
                Document = new()
                {
                    Rg = "12.123.123-2",
                    Cpf = "416.564.874-46"
                },
                Responsible = new()
                {
                    FirstName = "Ana",
                    LastName = "Bernardes",
                    Document = new()
                    {
                        Rg = "18.89.251-1",
                        Cpf = "456.987.233-45"
                    }
                }
            }, new GetAllStudentsDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "William",
                LastName = "Bonner da Silva",
                Responsible = new()
                {
                    FirstName = "João",
                    LastName = "de Jesus",
                    Document = new()
                    {
                        Rg = "48.879.251-4",
                        Cpf = "456.987.123-45"
                    }
                },Contact = new()
                {
                    CellPhone="(21) 94687-4697",
                    Landline="(21) 4679-1646",
                    Email="joao.jesus.12313@gmail.com"
                }
            },
            new GetAllStudentsDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "Luffy",
                LastName = "Monkey D.",
                Contract = new()
                {
                    Class = "EPCAR",
                    IsOwing = true
                },
                Responsible = new()
                {
                    FirstName = "Pedro",
                    LastName = "Paulo de Almeida",
                    Document = new()
                    {
                        Rg = "48.879.251-4",
                        Cpf = "456.987.123-45"
                    }
                }
            },
            new GetAllStudentsDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "Carlos",
                LastName = "Daniel",
                Contract = new()
                {
                    Class = "EPCAR",
                    IsOwing = true
                },
                Responsible = new()
                {
                    FirstName = "Pedro",
                    LastName = "Paulo de Almeida",
                    Document = new()
                    {
                        Rg = "48.879.251-4",
                        Cpf = "456.987.123-45"
                    }
                }
            },
            new GetAllStudentsDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "Carlos",
                LastName = "Daniel",
                Contract = new()
                {
                    Class = "EPCAR",
                    IsOwing = false
                },
                Document = new()
                {
                    Rg = "12.123.123-2",
                    Cpf = "456.564.874-46"
                },
                Responsible = new()
                {
                    FirstName = "Pedro",
                    LastName = "Paulo de Almeida",
                    Document = new()
                    {
                        Rg = "48.879.251-4",
                        Cpf = "456.987.123-45"
                    }
                }
            }
        };

        return await Task.FromResult(result);
    }
}
