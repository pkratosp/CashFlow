using CashFlow.Communication.requests;
using CashFlow.Communication.responses;

namespace CashFlow.Application.useCases.Expenses.Update;

public interface IUpdateExpanseUseCase
{
    Task Execute(long id,RequestUpdateExpenseJson body);
}
