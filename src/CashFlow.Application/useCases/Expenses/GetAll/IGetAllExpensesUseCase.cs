using CashFlow.Communication.responses;

namespace CashFlow.Application.useCases.Expenses.GetAll;

public interface IGetAllExpensesUseCase
{

    Task<ResponseExpensesJson> Execute();

}
