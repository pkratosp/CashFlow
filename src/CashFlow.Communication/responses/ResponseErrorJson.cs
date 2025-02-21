namespace CashFlow.Communication.responses
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessage { get; set; }

        public ResponseErrorJson(string errorMessage)
        {
            ErrorMessage = new List<string> { errorMessage };
        }

        public ResponseErrorJson(List<string> erroMessage)
        {
            ErrorMessage = erroMessage;
        }
    }
}
