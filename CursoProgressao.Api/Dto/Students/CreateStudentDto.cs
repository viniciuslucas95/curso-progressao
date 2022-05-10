﻿using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Dto.Responsibles;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Students;

public class CreateStudentDto
{
    [Required(ErrorMessage = "First name cannot be empty or null")]
    [MinLength(2, ErrorMessage = "First name must have at least 2 characters")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "Last name cannot be empty or null")]
    [MinLength(2, ErrorMessage = "Last name must have at least 2 characters")]
    public string LastName { get; set; } = null!;
    public ModifyDocumentDto? Document { get; set; }
    public ModifyResponsibleDto? Responsible { get; set; }
}
