using AutoMapper;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.useCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{

    private readonly IMapper _mapper;

    public RegisterUserUseCase(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson body)
    {
        Validate(body);
        var user = _mapper.Map<Domain.Entities.User>(body);

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = ""
        };
    }

    private void Validate(RequestRegisterUserJson body)
    {
        var result = new RegisterUserValidator().Validate(body);


        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}
