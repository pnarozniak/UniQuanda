using Microsoft.AspNetCore.Mvc.Filters;

namespace UniQuanda.Presentation.API.Attributes;

public class RecaptchaAttribute : Attribute
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}