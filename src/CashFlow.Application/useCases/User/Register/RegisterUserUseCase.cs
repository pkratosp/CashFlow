using AutoMapper;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.useCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{

    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userRepository;

    public RegisterUserUseCase(
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userRepository)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userRepository = userRepository;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson body)
    {
        Validate(body);
        var user = _mapper.Map<Domain.Entities.User>(body);
        user.Password = _passwordEncripter.Encript(body.Password);

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = ""
        };
    }

    private async Task Validate(RequestRegisterUserJson body)
    {
        var result = new RegisterUserValidator().Validate(body);

        var emailExist = await _userRepository.ExistActiveUserWithEmail(body.Email);

        if(emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}
