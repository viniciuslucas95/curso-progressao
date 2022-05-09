using Api.Data.Contexts;
using Api.Data.UnitOfWork;
using Api.Filters;
using Api.Middlewares;
using Api.Repositories.Students;
using Api.Services.Students;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionString"])
);

builder.Services.AddTransient<ExceptionHandlerMiddleware>();
builder.Services.AddTransient<IUnitOfWork, SchoolUnitOfWork>();
builder.Services.AddTransient<IStudentsRepository, StudentsRepository>();
builder.Services.AddTransient<IStudentsService, StudentsService>();

builder.Services.AddControllers(options =>
    options.Filters.Add(new ModelExceptionHandler())
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
