using SummerSchool.ApplicationSystems.Shared.Models;

namespace SummerSchool.ApplicationSystems.Core.Services.Token;

public interface ITokenService
{
    (string, DateTime) BuildToken(UserModel user);
    bool ValidateToken(string token);
}
