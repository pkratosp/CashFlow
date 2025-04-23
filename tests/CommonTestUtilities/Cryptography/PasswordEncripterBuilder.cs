using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();
        _mock.Setup(passwordEncripter => passwordEncripter.Encript(It.IsAny<string>())).Returns("34290347ewff!");
    }

    public PasswordEncripterBuilder Verify(string? password)
    {
        if (string.IsNullOrWhiteSpace(password) == false)
        {
            _mock.Setup(passwordEncripter => passwordEncripter.Verify(password, It.IsAny<string>())).Returns(true);
        }

        return this;
    }

    public IPasswordEncripter Build()
    {
        return _mock.Object;
    }
}
