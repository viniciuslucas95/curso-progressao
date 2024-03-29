﻿using CursoProgressao.Shared.Dto.Students;

namespace CursoProgressao.Server.Services.Students;

public interface IStudentsService
{
    Task<Guid> CreateAsync(CreateStudentDto dto);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateStudentDto dto);
    Task<GetAllPartialStudentsDto> GetAllAsync(GetAllStudentsQueryDto query);
    Task CheckExistenceAsync(Guid id);
}
