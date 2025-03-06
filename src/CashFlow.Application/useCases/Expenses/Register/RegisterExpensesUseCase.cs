using CashFlow.Communication.enums;
using CashFlow.Communication.requests;
using CashFlow.Communication.responses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.useCases.Expenses.Register;
public class RegisterExpensesUseCase
{
    public ResponseRegisteredExpensesJson Execute(RequestRegisterExpenseJson body) 
    {
        Validate(body: body);

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

