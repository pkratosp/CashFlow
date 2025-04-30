using CashFlow.Domain.Security.Token;

namespace CashFlow.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string TokenOnRequest()
    {
        var authorization = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        // os 2 pontos pega tudo depois do Bearer 
        return authorization["Bearer ".Length..].Trim();
    }
}
