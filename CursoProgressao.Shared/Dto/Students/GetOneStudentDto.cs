﻿using CursoProgressao.Shared.Dto.Contacts;
using CursoProgressao.Shared.Dto.Documents;
using CursoProgressao.Shared.Dto.Residences;
using CursoProgressao.Shared.Dto.Responsibles;

namespace CursoProgressao.Shared.Dto.Students;

public class GetOneStudentDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Note { get; set; }
    public bool IsActive { get; set; }
    public GetOneDocumentDto? Document { get; set; } = null!;
    public GetOneResponsibleDto? Responsible { get; set; } = null!;
    public GetOneContactDto? Contact { get; set; } = null!;
    public GetOneResidenceDto? Residence { get; set; } = null!;
}