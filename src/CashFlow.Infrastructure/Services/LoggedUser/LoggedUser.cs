using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Token;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly CashFlowDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(CashFlowDbContext cashFlowDbContext, ITokenProvider tokenProvider)
    {
        _dbContext = cashFlowDbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();


        var securityToken = tokenHandler.ReadJwtToken(token);

        var identify = securityToken.Claims.First(claim => claim.Type == "sub").Value;

            var findUser = await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentify == Guid.Parse(identify));
        return findUser;
    }
}
