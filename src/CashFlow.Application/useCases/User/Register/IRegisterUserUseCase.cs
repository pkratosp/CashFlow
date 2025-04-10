using CashFlow.Communication.requests;
using CashFlow.Communication.responses;

namespace CashFlow.Application.useCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson body);
}
