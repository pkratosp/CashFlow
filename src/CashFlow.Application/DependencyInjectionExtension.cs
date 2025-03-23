using CashFlow.Application.AutoMapper;
using CashFlow.Application.useCases.Expenses.Register;
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
    }
}
