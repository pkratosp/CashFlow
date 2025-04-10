using BC = BCrypt.Net.BCrypt;
using CashFlow.Domain.Security.Cryptography;

namespace CashFlow.Infrastructure.Security.Cryptography;

public class Bycript : IPasswordEncripter
{
    public string Encript(string password)
    {
        var passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

}
