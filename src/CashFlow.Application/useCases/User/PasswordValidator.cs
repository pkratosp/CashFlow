﻿using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.useCases.User;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        return false;
    }
}
