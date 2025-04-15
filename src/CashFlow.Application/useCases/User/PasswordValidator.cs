using System.Text.RegularExpressions;
using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.useCases.User;
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";
    private readonly Regex _upperCaseLetter = new Regex(@"[A-Z]+");
    private readonly Regex _lowerCaseLetter = new Regex(@"[a-z]+");
    private readonly Regex _numbers = new Regex(@"[0-9]+");
    private readonly Regex _specialSymbols = new Regex(@"[\!\?\*\.]+");

    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if (!_upperCaseLetter.IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if (!_lowerCaseLetter.IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if (!_numbers.IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if (!_specialSymbols.IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        return true;
    }
}