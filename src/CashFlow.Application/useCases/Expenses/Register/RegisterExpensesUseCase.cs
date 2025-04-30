using AutoMapper;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.useCases.Expenses.Register;
public class RegisterExpensesUseCase: IRegisterExpensesUseCase
{

    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterExpensesUseCase(
        IExpensesWriteOnlyRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser
    )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseRegisteredExpensesJson> Execute(RequestRegisterExpenseJson body) 
    {
        Validate(body: body);

        var loggedUser = await _loggedUser.Get();

        var entity = _mapper.Map<Expense>(body);
        entity.UserId = loggedUser.Id;

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredExpensesJson>(entity);
    }


    private void Validate(RequestRegisterExpenseJson body) 
    {
        var validator = new RegisterExpensesValidator();
        var result = validator.Validate(body);

        if (result.IsValid == false)
        {
            var errosMessage = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorValidationException(errosMessage);
            //throw new ArgumentException(errosMessage);
        }
    }
}

