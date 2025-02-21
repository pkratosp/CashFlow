namespace CashFlow.Communication.responses
{
    public class ResponseErrorJson
    {
        public string ErrorMessage { get; set; } = String.Empty;

        public ResponseErrorJson(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
