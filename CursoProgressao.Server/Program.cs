using CursoProgressao.Server.Data;
using CursoProgressao.Server.Filters;
using CursoProgressao.Server.Middlewares;
using CursoProgressao.Server.Services.Classes;
using CursoProgressao.Server.Services.Responsibles;
using CursoProgressao.Server.Services.Students;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ExceptionHandlerMiddleware>();
builder.Services.AddTransient<IClassesService, ClassesService>();
builder.Services.AddTransient<IResponsiblesService, ResponsiblesService>();
builder.Services.AddTransient<IStudentsService, StudentsService>();

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionString"])
);

builder.Services.AddControllers(options
    => options.Filters.Add(new ModelExceptionFilter()))
    .ConfigureApiBehaviorOptions(options
        => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
