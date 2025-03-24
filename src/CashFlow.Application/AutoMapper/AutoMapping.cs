using AutoMapper;
using CashFlow.Communication.responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class AutoMapping: Profile
{

    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<ResponseRegisteredExpensesJson, Expense>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpensesJson>();
        CreateMap<Expense, ResponseShortExpensesJson>();
    }

}
