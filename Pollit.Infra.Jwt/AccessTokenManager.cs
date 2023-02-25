using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pollit.Domain._Ports;
using Pollit.Domain.Users;

namespace Pollit.Infra.Jwt;

public class AccessTokenManager : IAccessTokenManager
{
    private readonly JwtConfig _config;
    private readonly SymmetricSecurityKey _symmetricSecurityKey;

    public AccessTokenManager(JwtConfig config)
    {
        _config = config;
        _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey));
    }

    public AccessToken Build(IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            _config.Issuer,
            _config.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_config.ExpiryInMinutes),
            signingCredentials: new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        return new AccessToken(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public bool IsValid(AccessToken accessToken, out Dictionary<string, string> decodedToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _symmetricSecurityKey,
            ValidAudience = _config.Audience,
            ValidateAudience = true,
            ValidIssuer = _config.Issuer,
            ValidateIssuer = true,
        };

        decodedToken = new Dictionary<string, string>();
        
        SecurityToken validatedToken;
        try
        {
            decodedToken = tokenHandler.ValidateToken(accessToken.Value, validationParameters, out validatedToken).Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
        }
        catch (Exception)
        {
            return false;
        }

        return validatedToken != null;
    }

    public IEnumerable<Claim> Decrypt(AccessToken accessToken)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _symmetricSecurityKey,
            ValidAudience = _config.Audience,
            ValidateAudience = true,
            ValidIssuer = _config.Issuer,
            ValidateIssuer = true,
            ValidateLifetime = false
        };

        try
        {
            return new JwtSecurityTokenHandler().ValidateToken(accessToken.Value, validationParameters, out _).Claims;
        }
        catch (Exception)
        {
            return ArraySegment<Claim>.Empty;
        }
    }
}

public class JwtConfig
{
    public int ExpiryInMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}