﻿using CashFlow.Communication.enums;
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
        //var titleIsEmpty = string.IsNullOrEmpty(body.Title);

        //if (titleIsEmpty)
        //{
        //    throw new ArgumentException("The title is required");
        //}

        //if(body.Amount <= 0)
        //{
        //    throw new ArgumentException("The amount must be greater than zero");
        //}

        //var result = DateTime.Compare(body.Date, DateTime.UtcNow);
        //if (result > 0) 
        //{
        //    throw new ArgumentException("Expenses cannot be for the future");
        //}

        //var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), body.PaymentType);
        //if (paymentTypeIsValid == false)
        //{
        //    throw new ArgumentException("PaymentType is not valid");
        //}
    }
}

