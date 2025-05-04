
using AutoMapper;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.useCases.Expenses.Delete;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository;
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteExpenseUseCase(
        IExpensesReadOnlyRepository expensesReadOnlyRepository,
        IExpensesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser
    )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _expensesReadOnlyRepository = expensesReadOnlyRepository;
    }

    public async Task Execute(long id)
    {
        var user = await _loggedUser.Get();

        var expense = await _expensesReadOnlyRepository.GetById(user, id);

        if (expense is null)
        {
            throw new NotFoundExpection(ResourceErrorMessages.NOT_FOUND);
        }

        await _repository.Delete(id);

        await _unitOfWork.Commit();
    }
}
