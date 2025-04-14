﻿using System.Security.Claims;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Token;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace CashFlow.Infrastructure.Security.Tokens;

public class JwtTokensGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signinKey;

    public JwtTokensGenerator(uint expirationTimeMinutes, string signinKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signinKey = signinKey;
    }

    public string Generate(User user)
    {

        var claims = new List<Claim>() 
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("sub", user.Id.ToString())
        };

        var tokenDescription = new SecurityTokenDescriptor 
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecureKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };


        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecureKey()
    {
        var key = Encoding.UTF8.GetBytes(_signinKey);

        return new SymmetricSecurityKey(key);
    }
}
