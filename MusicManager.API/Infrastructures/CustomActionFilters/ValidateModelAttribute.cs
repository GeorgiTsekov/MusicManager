﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicManager.API.Infrastructures.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }
            base.OnActionExecuted(context);
        }
    }
}
