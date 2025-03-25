using CashFlow.Communication.responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }else
        {
            Console.WriteLine(context.ToString());
            ThrowUnknowError(context);
        }
    }

    private void HandleProjectException (ExceptionContext context) 
    {
        if (context.Exception is ErrorValidationException errorValidationException)
        {
            var errorMessage = new ResponseErrorJson(errorValidationException.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorMessage);
        } else if (context.Exception is NotFoundExpection notFoundExpection) 
        { 
            var errorMessage = new ResponseErrorJson(notFoundExpection.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new NotFoundObjectResult(errorMessage);
        } else
        {
            ThrowUnknowError(context);
        }
    }

    private void ThrowUnknowError(ExceptionContext context) 
    {
        var errorMessage = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorMessage);
    }
}