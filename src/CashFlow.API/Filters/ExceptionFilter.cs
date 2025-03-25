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
            ThrowUnknowError(context);
        }
    }

    private void HandleProjectException (ExceptionContext context) 
    {
        var cashFlowExcepetion = (CashFlowException)context.Exception;
        var errorMessage = new ResponseErrorJson(cashFlowExcepetion.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowExcepetion.StatusCode;
        context.Result = new ObjectResult(errorMessage);
    }

    private void ThrowUnknowError(ExceptionContext context) 
    {
        var errorMessage = new ResponseErrorJson(ResourceErrorMessages.UNKNOW_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorMessage);
    }
}