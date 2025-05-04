using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteOnlyRepository
{
    Task Add(Expense expense);

    /// <summary>
    /// Esta função retorna TRUE se a exclusão foi bem-sucedida, caso contrário, retorna FALSE
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(long id);
}
