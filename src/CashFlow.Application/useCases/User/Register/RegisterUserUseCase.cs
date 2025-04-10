using AutoMapper;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Domain.Repositories;
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
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userRepository = userRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson body)
    {
        await Validate(body);
        var user = _mapper.Map<Domain.Entities.User>(body);
        user.Password = _passwordEncripter.Encript(body.Password);
        user.UserIdentify = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

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

        if (result.IsValid == true)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}
