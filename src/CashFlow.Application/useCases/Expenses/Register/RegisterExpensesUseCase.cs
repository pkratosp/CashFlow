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

    public RegisterExpensesUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public ResponseRegisteredExpensesJson Execute(RequestRegisterExpenseJson body) 
    {
        Validate(body: body);

        var entity = new Expense {
            Title = body.Title,
            Amount = body.Amount,
            Date = body.Date,
            Description = body.Description,
            PaymentType = (Domain.enums.PaymentType)body.PaymentType
        };

        _repository.Add(entity);

        _unitOfWork.Commit();

        return new ResponseRegisteredExpensesJson();
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

