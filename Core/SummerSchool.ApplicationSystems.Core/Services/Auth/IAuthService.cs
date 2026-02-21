using SummerSchool.ApplicationSystems.Core.DTOs.Auth.Request;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Shared.Models;

namespace SummerSchool.ApplicationSystems.Core.Services.Auth;

public interface IAuthService
{
    Task<ServiceResponseDto<UserModel>> StudentLoginAsync(StudentLoginRequestDto request, CancellationToken cancellationToken);
    ServiceResponseDto<UserModel> AdminLogin(AdminLoginRequestDto request);
}
