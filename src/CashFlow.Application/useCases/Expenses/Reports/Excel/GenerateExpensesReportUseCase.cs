using CashFlow.Domain.enums;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.useCases.Expenses.Reports.Excel;

public class GenerateExpensesReportUseCase : IGenerateExpensesReportUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;

    public GenerateExpensesReportUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;   
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);

        if (expenses.Count == 0)
        {
            return [];
        }

        var workbook = new XLWorkbook();

        workbook.Author = "Pedro";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var rows = 2;
        foreach(var expense in expenses)
        {
            worksheet.Cell($"A{rows}").Value = expense.Title;
            worksheet.Cell($"B{rows}").Value = expense.Date;
            worksheet.Cell($"C{rows}").Value = FormatePayment(expense.PaymentType);
            worksheet.Cell($"D{rows}").Value = expense.Amount;
            worksheet.Cell($"E{rows}").Value = expense.Description;

            rows++;
        }

        var file = new MemoryStream();

        workbook.SaveAs(file);

        return file.ToArray();
    }

    private string FormatePayment(PaymentType payment)
    {
        return payment switch 
        {
            PaymentType.Cach => "Dinheiro",
            PaymentType.CreditCard => "Cartão de crédito",
            PaymentType.DebitCard => "Cartão de débito",
            PaymentType.EletronicTransfer => "Transaferencia eletrónica",
            _ => string.Empty
        
        };
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;


        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

    }
}
