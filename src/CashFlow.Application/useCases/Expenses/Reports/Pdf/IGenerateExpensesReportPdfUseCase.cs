namespace CashFlow.Application.useCases.Expenses.Reports.Pdf;

public interface IGenerateExpensesReportPdfUseCase
{

    Task<byte[]> Execute(DateOnly month);

}
