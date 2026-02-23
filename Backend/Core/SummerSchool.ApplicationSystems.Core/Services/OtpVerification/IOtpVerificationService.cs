using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.OtpVerification.Request;
using SummerSchool.ApplicationSystems.Core.DTOs.OtpVerification.Response;
using SummerSchool.ApplicationSystems.Core.Services.Base;

namespace SummerSchool.ApplicationSystems.Core.Services.OtpVerification;

public interface IOtpVerificationService : IBaseService<Entities.OtpVerification>
{
    Task<ServiceResponseDto<OtpVerificationResponseDto>> CreateOtpAsync(CreateOtpRequestDto request, CancellationToken cancellationToken);
    ServiceResponseDto VerifyOtp(string code);
}
