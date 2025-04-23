using CashFlow.Communication.requests;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestLoginJsonBuilder
{
    public static RequestUserLogin Build()
    {
        return new Faker<RequestUserLogin>()
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}
