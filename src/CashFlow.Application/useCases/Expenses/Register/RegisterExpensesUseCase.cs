using AutoMapper;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.useCases.Expenses.Register;
public class RegisterExpensesUseCase: IRegisterExpensesUseCase
{

    private readonly IExpensesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterExpensesUseCase(
        IExpensesRepository repository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredExpensesJson> Execute(RequestRegisterExpenseJson body) 
    {
        Validate(body: body);

        var entity = _mapper.Map<Expense>(body);

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

