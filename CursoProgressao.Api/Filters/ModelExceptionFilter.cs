﻿using CursoProgressao.Api.Dto.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace CursoProgressao.Api.Filters
{
    public class ModelExceptionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ErrorDto<ErrorItemWithModelDto> result = new();
                string pattern = "(?:!--!)";

                foreach (var modelStateValue in context.ModelState.Values)
                {
                    foreach (var errorModel in modelStateValue.Errors)
                    {
                        string[] texts = Regex.Split(errorModel.ErrorMessage, pattern);
                        string name = texts[0];
                        string message = "";
                        string model = "";

                        if (texts.Length > 1) message = texts[1];
                        if (texts.Length > 2) model = texts[2];

                        result.Errors.Add(new()
                        {
                            Name = name,
                            Message = message,
                            Model = model
                        });
                    }
                }

                context.Result = new BadRequestObjectResult(result);
            }
        }
    }
}