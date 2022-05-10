using CursoProgressao.Api.Dto.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CursoProgressao.Api.Filters
{
    public class ModelExceptionHandler : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto result = new();

                foreach (var modelStateValue in context.ModelState.Values)
                {
                    foreach (var errorModel in modelStateValue.Errors)
                    {
                        result.Errors.Add(errorModel.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(result);
            }
        }
    }
}
