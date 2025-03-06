using CashFlow.Communication.requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.useCases.Expenses.Register
{
    public class RegisterExpensesValidator: AbstractValidator<RequestRegisterExpenseJson>
    {
        public RegisterExpensesValidator()
        {
            RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
            RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
            RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.DATE_INVALID);
            RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENTTYPE_INVALID);
        }

    }
}
