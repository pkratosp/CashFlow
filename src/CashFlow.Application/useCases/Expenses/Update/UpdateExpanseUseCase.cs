
using AutoMapper;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using System.ComponentModel.DataAnnotations;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.useCases.Expenses.Update;

public class UpdateExpanseUseCase : IUpdateExpanseUseCase
{
    private readonly IExpensesUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public UpdateExpanseUseCase(
        IExpensesUpdateOnlyRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser
    )
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id, RequestUpdateExpenseJson body)
    {
        Validate(body);

        var loggedUser = await _loggedUser.Get();

        var expense = await _repository.GetById(id);
      
        if (expense is null || expense.UserId != loggedUser.Id)
        {
            throw new NotFoundExpection(ResourceErrorMessages.NOT_FOUND);
        }

        _mapper.Map(body, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestUpdateExpenseJson body)
    {
        var validator = new UpdateExpenseValidator();
        var result = validator.Validate(body);

        if(result.IsValid == false)
        {
            var errorsMessage = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorValidationException(errorsMessage);
        }
    }
}
