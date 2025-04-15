using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.useCases.Login;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _token;

    public LoginUserUseCase(IPasswordEncripter passwordEncripter, IUserReadOnlyRepository userReadOnlyRepository, IAccessTokenGenerator token)
    {
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _token = token;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestUserLogin body)
    {
        var user = await _userReadOnlyRepository.GetByEmail(body.Email);

        if(user is null)
        {
            throw new InvalidLoginException();
        }

        var verifyMathPasswordHash = _passwordEncripter.Verify(body.Password, user.Password);
    
        if(verifyMathPasswordHash is false)
        {
            throw new InvalidLoginException();
        }


        return new ResponseRegisterUserJson 
        {
            Name = user.Name,
            Token = _token.Generate(user)
        };

    }
}
