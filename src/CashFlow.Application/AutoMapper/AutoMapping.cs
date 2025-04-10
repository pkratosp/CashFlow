using AutoMapper;
using CashFlow.Communication.requests;
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
        CreateMap<RequestRegisterExpenseJson, Expense>();
        CreateMap<RequestUpdateExpenseJson, Expense>();
        CreateMap<RequestRegisterUserJson, User>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpensesJson>();
        CreateMap<Expense, ResponseShortExpensesJson>();
        CreateMap<Expense, ResponseExpenseJson>();
        CreateMap<User, ResponseRegisterUserJson>();
    }

}
