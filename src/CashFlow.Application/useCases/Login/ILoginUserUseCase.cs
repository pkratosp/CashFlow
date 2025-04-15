using CashFlow.Communication.requests;
using CashFlow.Communication.responses;

namespace CashFlow.Application.useCases.Login;
public interface ILoginUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestUserLogin body);
}
