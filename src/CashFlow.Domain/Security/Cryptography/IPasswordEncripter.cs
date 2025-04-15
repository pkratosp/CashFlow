﻿namespace CashFlow.Domain.Security.Cryptography;
public interface IPasswordEncripter
{
    string Encript(string password);
    bool Verify(string password, string hashPassword);
}
