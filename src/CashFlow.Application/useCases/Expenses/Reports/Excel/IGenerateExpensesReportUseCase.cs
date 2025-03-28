namespace CashFlow.Application.useCases.Expenses.Reports.Excel;

public interface IGenerateExpensesReportUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
