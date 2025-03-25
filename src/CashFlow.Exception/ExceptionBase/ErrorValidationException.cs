using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class ErrorValidationException : CashFlowException
{
    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorValidationException(List<string> errorsMessage) : base (string.Empty) { 
        _errors = errorsMessage;
    }

    public override List<string> GetErrors()
    {
        return _errors;

    }
}
