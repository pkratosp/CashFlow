using CashFlow.Domain.Security.Cryptography;
using CashFlow.Infrastructure.DataAccess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private CashFlow.Domain.Entities.User _user { get; set; }
    private string _userPassword { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services => 
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                
                services.AddDbContext<CashFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();

                StartDatabase(dbContext, passwordEncripter);
            });
    }

    public string GetGemail() => _user.Email;

    public string GetPassword() => _userPassword;
    public string GetName() => _user.Name;

    private void StartDatabase(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter)
    {
        _user = UserBuilder.Build();
        _userPassword = _user.Password;
        _user.Password = passwordEncripter.Encript(_user.Password);

        dbContext.Users.Add(_user);

        dbContext.SaveChanges();
    }
}
