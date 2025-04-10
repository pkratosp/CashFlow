using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration cofiguration)
    {
        AddRepositories(services);
        AddContext(services, cofiguration);

        services.AddScoped<IPasswordEncripter, Bycript>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddContext(IServiceCollection serivces, IConfiguration cofiguration)
    {

        var connectionString = cofiguration.GetConnectionString("Connection"); ;

        var version = new Version(8, 0, 35);
        var serverVersion = new MySqlServerVersion(version);

        serivces.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }

}
