using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class NotFoundExpection : CashFlowException
{

    public NotFoundExpection(string message) : base(message) { }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors ()
    {
        return [Message];
    }

}
