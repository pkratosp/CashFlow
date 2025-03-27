
using AutoMapper;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Application.useCases.Expenses.Update;

public class UpdateExpanseUseCase : IUpdateExpanseUseCase
{
    private readonly IExpensesUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateExpanseUseCase(IExpensesUpdateOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id, RequestUpdateExpenseJson body)
    {
        Validate(body);

        var expense = await _repository.GetById(id);

        if (expense is null)
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
