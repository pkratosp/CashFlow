﻿using CashFlow.Application.useCases.User;
using FluentValidation;
using CashFlow.Communication.requests;
using FluentAssertions;

namespace Validators.Tests.User;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Error_Password_Invalid(string password)
    {
        var validator = new PasswordValidator<RequestRegisterUserJson>();
        
        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);

        result.Should().BeFalse();
    }
}
