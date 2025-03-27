using CashFlow.Communication.enums;

namespace CashFlow.Communication.requests;

public class RequestUpdateExpenseJson
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public PaymentType PaymentType { get; set; }

}
