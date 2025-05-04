using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Extensions;
using CashFlow.Infrastructure.Security.Cryptography;
using CashFlow.Infrastructure.Security.Tokens;
using CashFlow.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration cofiguration)
    {
        services.AddScoped<IPasswordEncripter, Bycript>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddRepositories(services);
        AddToken(services, cofiguration);

        if(cofiguration.IsTestEnviroment() == false)
        {
            AddContext(services, cofiguration);
        }
    }

    private static void AddToken (IServiceCollection services, IConfiguration configuration)
    {
        var expiresTimeMinutes = configuration.GetValue<uint>("Settings:JWT:ExpiresTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:JWT:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokensGenerator(expiresTimeMinutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddContext(IServiceCollection serivces, IConfiguration cofiguration)
    {

        var connectionString = cofiguration.GetConnectionString("Connection"); ;

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        serivces.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }

}
