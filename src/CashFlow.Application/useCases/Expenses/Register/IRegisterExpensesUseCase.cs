using CashFlow.Communication.requests;
using CashFlow.Communication.responses;

namespace CashFlow.Application.useCases.Expenses.Register;
public interface IRegisterExpensesUseCase
{
    Task<ResponseRegisteredExpensesJson> Execute(RequestRegisterExpenseJson body);

}
