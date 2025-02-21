using CashFlow.Communication.requests;
using FluentValidation;

namespace CashFlow.Application.useCases.Expenses.Register
{
    public class RegisterExpensesValidator: AbstractValidator<RequestRegisterExpenseJson>
    {
        public RegisterExpensesValidator()
        {
            RuleFor(expense => expense.Title).NotEmpty().WithMessage("The title is required");
            RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("The amount must be greater than zero");
            RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The amount must be greater than zero");
            RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("PaymentType is not valid");
        }

    }
}
