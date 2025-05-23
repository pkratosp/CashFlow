﻿using CashFlow.Communication.requests;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{

    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, faker => faker.Person.UserName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }

}
