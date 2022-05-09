using Api.Dto.Common;
using Api.Exceptions.Base;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
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

                await context.Response.WriteAsync(CreateError(new InternalServerErrorException(ex.Message)));
            }
        }

        private string CreateError(BaseException ex)
        {
            ErrorDto error = new();
            error.Errors.Add(ex.Message);

            return JsonSerializer.Serialize(error);
        }
    }
}
