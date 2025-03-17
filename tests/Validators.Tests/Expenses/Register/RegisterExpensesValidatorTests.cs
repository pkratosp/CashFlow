using CashFlow.Application.useCases.Expenses.Register;
using CashFlow.Communication.requests;
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
    }
}
