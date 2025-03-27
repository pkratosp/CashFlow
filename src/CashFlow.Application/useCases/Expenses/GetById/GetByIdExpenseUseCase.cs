using AutoMapper;
using CashFlow.Communication.responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.useCases.Expenses.GetById;

public class GetByIdExpenseUseCase : IGetByIdExpenseUseCase
{

    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdExpenseUseCase(IExpensesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson?> Execute(long id)
    {
        
        var result = await _repository.GetById(id);

        if (result is null)
        {
            throw new NotFoundExpection(ResourceErrorMessages.NOT_FOUND);
        }

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
