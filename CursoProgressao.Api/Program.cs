using CursoProgressao.Api.Data.Contexts;
using CursoProgressao.Api.Data.UnitOfWork;
using CursoProgressao.Api.Filters;
using CursoProgressao.Api.Middlewares;
using CursoProgressao.Api.Repositories.Classes;
using CursoProgressao.Api.Repositories.Students;
using CursoProgressao.Api.Services.Classes;
using CursoProgressao.Api.Services.Contacts;
using CursoProgressao.Api.Services.Residences;
using CursoProgressao.Api.Services.ResponsibleDocuments;
using CursoProgressao.Api.Services.Responsibles;
using CursoProgressao.Api.Services.StudentDocuments;
using CursoProgressao.Api.Services.Students;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionString"])
);

builder.Services.AddTransient<ExceptionHandlerMiddleware>();
builder.Services.AddTransient<SchoolUnitOfWork>();
builder.Services.AddTransient<IStudentsRepository, StudentsRepository>();
builder.Services.AddTransient<IClassesRepository, ClassesRepository>();
builder.Services.AddTransient<IClassesService, ClassesService>();
builder.Services.AddTransient<IStudentsService, StudentsService>();
builder.Services.AddTransient<IStudentDocumentsService, StudentDocumentsService>();
builder.Services.AddTransient<IResponsiblesService, ResponsiblesServices>();
builder.Services.AddTransient<IResponsibleDocumentsService, ResponsibleDocumentsService>();
builder.Services.AddTransient<IContactsService, ContactsService>();
builder.Services.AddTransient<IResidencesService, ResidencesService>();

builder.Services.AddControllers(options =>
    options.Filters.Add(new ModelExceptionFilter())
)
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
