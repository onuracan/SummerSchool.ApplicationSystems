using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SummerSchool.ApplicationSystems.Core.Services.Token;
using SummerSchool.ApplicationSystems.Service.Infrastructure.Configurations;
using SummerSchool.ApplicationSystems.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SummerSchool.ApplicationSystems.Service.Services.Token;

public class TokenService(IOptions<JwtIssuerSettings> options) : ITokenService
{
    private readonly JwtIssuerSettings _jwtIssuerSettings = options.Value;

    public (string, DateTime) BuildToken(UserModel user)
    {
        var newExpireTime = DateTime.Now.AddHours(this._jwtIssuerSettings.ExpireTime);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(this.GetUserClaims(user)),
            Expires = newExpireTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtIssuerSettings.Key)), SecurityAlgorithms.HmacSha256Signature),
            Issuer = this._jwtIssuerSettings.Issuer,
            Audience = this._jwtIssuerSettings.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);
        return (jwt, newExpireTime);
    }

    public bool ValidateToken(string token)
    {
        var mySecret = Encoding.UTF8.GetBytes(this._jwtIssuerSettings.Key);
        var mySecurityKey = new SymmetricSecurityKey(mySecret);
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken = null;

        tokenHandler.ValidateToken(token,
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = this._jwtIssuerSettings.Issuer,
            ValidAudience = this._jwtIssuerSettings.Audience,
            IssuerSigningKey = mySecurityKey,
        }, out validatedToken);

        return validatedToken == null;
    }

    public List<Claim> GetUserClaims(UserModel user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("UserType", user.UserType.ToString())
        };

        if (!string.IsNullOrEmpty(user.UserName))
            claims.Add(new Claim("UserName", user.UserName));

        if (!string.IsNullOrEmpty(user.NameAndSurname))
            claims.Add(new Claim("NameAndSurname", user.NameAndSurname));

        if (!string.IsNullOrEmpty(user.PhoneNumber))
            claims.Add(new Claim("PhoneNumber", user.PhoneNumber));

        if (!string.IsNullOrEmpty(user.EMail))
            claims.Add(new Claim("EMail", user.EMail));

        return claims;
    }
}
