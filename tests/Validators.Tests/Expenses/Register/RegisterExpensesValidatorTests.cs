using CashFlow.Application.useCases.Expenses.Register;
using CashFlow.Communication.enums;
using CashFlow.Communication.requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register
{
    public class RegisterExpensesValidatorTests
    {
        [Fact]
        public void Success()
        {

            // Arrange
            var validator = new RegisterExpensesValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();


            // Act
            var result = validator.Validate(request);

            // Assert
            //Assert.NotNull(result);
            //Assert.True(result.IsValid);
            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void ErrorTitleEmpty()
        {

            var validator = new RegisterExpensesValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();

            request.Title = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain((e) => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
        }

        [Fact]
        public void ErrorDateFuture()
        {
            var validator = new RegisterExpensesValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();

            request.Date = DateTime.UtcNow.AddDays(1);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain((e) => e.ErrorMessage.Equals(ResourceErrorMessages.DATE_INVALID));

        }

        [Fact]
        public void ErrorPaymentNotValid()
        {
            var validator = new RegisterExpensesValidator();

            var request = RequestRegisterExpenseJsonBuilder.Build();

            request.PaymentType = (PaymentType)99;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain((e) => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENTTYPE_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-6)]
        [InlineData(-4)]
        public void ErrorAmountInvalid(decimal amount)
        {
            var validator = new RegisterExpensesValidator();

            var request = RequestRegisterExpenseJsonBuilder.Build();

            request.Amount = amount;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain((e) => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));

        }
    }
}
