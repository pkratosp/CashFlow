using CashFlow.Communication.responses;

namespace CashFlow.Application.useCases.Expenses.GetById;

public interface IGetByIdExpenseUseCase
{
    Task<ResponseExpenseJson?> Execute(long id);
}
