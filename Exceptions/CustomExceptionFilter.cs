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
        else if (context.Exception is EntityNotFound questionNotFound)
        {
            context.Result = new ObjectResult(new { message = questionNotFound.Message })
            {
                StatusCode = 404
            };
        }
        else if (context.Exception is OperationNotAllowed operationNotAllowed)
        {
            context.Result = new ObjectResult(new { message = operationNotAllowed.Message })
            {
                StatusCode = 403
            };
        }
        else if (context.Exception is OidcUserMappingAlreadyCreated oidcUserMappingAlreadyCreated)
        {
            context.Result = new ObjectResult(new { message = oidcUserMappingAlreadyCreated.Message })
            {
                StatusCode = 400
            };
        }
    }
}