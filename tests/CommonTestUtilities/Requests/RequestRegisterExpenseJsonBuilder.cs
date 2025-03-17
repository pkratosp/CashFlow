

using Bogus;
using CashFlow.Communication.enums;
using CashFlow.Communication.requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterExpenseJsonBuilder
    {

        public static RequestRegisterExpenseJson Build() 
        {
            var faker = new Faker();

            return new Faker<RequestRegisterExpenseJson>()
                .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
                .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(r => r.Date, faker => faker.Date.Past())
                .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
                .RuleFor(r => r.Amount, faker => decimal.Parse(faker.Commerce.Price()));

            //return new RequestRegisterExpenseJson 
            //{
            //    Amount = decimal.Parse(faker.Commerce.Price()),
            //    Date = faker.Date.Past(),
            //    Description = "",
            //    PaymentType = CashFlow.Communication.enums.PaymentType.CreditCard,
            //    Title = faker.Commerce.Product()
            //};
        }

    }
}
