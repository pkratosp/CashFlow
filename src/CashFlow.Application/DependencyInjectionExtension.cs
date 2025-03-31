using CashFlow.Application.AutoMapper;
using CashFlow.Application.useCases.Expenses.Delete;
using CashFlow.Application.useCases.Expenses.GetAll;
using CashFlow.Application.useCases.Expenses.GetById;
using CashFlow.Application.useCases.Expenses.Register;
using CashFlow.Application.useCases.Expenses.Reports.Excel;
using CashFlow.Application.useCases.Expenses.Reports.Pdf;
using CashFlow.Application.useCases.Expenses.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static void AddAplication(this IServiceCollection services)
    {
        AddUseCase(services);
        AddAutoMapper(services);
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    public static void AddUseCase(this IServiceCollection services)
    {
        services.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
        services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
        services.AddScoped<IGetByIdExpenseUseCase, GetByIdExpenseUseCase>();
        services.AddScoped<IUpdateExpanseUseCase, UpdateExpanseUseCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        services.AddScoped<IGenerateExpensesReportUseCase, GenerateExpensesReportUseCase>();
        services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
    }
}
