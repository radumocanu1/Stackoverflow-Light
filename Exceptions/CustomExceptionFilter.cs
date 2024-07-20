using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Stackoverflow_Light.Exceptions;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is OidcUserMappingNotFound oidcUserMappingNotFound)
        {
            context.Result = new ObjectResult(new { message = oidcUserMappingNotFound.Message })
            {
                StatusCode = 400
            };
        }
        else if (context.Exception is ArgumentException argumentException)
        {
            context.Result = new ObjectResult(new { message = argumentException.Message })
            {
                StatusCode = 400
            };
        }
        // else
        // {
        //     context.Result = new ObjectResult(new { message = "An unexpected error occurred." })
        //     {
        //         StatusCode = 500
        //     };
        // }
        // context.ExceptionHandled = true;
    }
}