using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Shared.Dto.Errors;
using System.Net;
using System.Text.Json;

namespace CursoProgressao.Server.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly SchoolContext _context;

    public ExceptionHandlerMiddleware(SchoolContext context) => _context = context;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BaseException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(CreateError(ex));

        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";

            Error error = new("InternalServerError", ex.Message);
            _context.Errors.Add(error);
            await _context.SaveChangesAsync();

            await context.Response.WriteAsync(CreateError(new InternalServerErrorException("InternalServerError", ex.Message)));
        }
    }

    private static string CreateError(BaseException ex)
    {
        ErrorDto error = new();
        error.Errors.Add(new()
        {
            Name = ex.Name,
            Message = ex.Message
        });

        return JsonSerializer.Serialize(error);
    }
}
