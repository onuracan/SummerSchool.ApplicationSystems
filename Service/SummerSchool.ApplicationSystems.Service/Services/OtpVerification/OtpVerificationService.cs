using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using SummerSchool.ApplicationSystems.Core.Constants;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.OtpVerification.Request;
using SummerSchool.ApplicationSystems.Core.DTOs.OtpVerification.Response;
using SummerSchool.ApplicationSystems.Core.Enums;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.OtpVerification;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.OtpVerification;

public class OtpVerificationService(IBaseRepository<Entities.OtpVerification> repository, IMapper mapper, ILogger<OtpVerificationService> logger) : BaseService<Entities.OtpVerification>(repository), IOtpVerificationService
{
    private readonly IBaseRepository<Entities.OtpVerification> _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<OtpVerificationService> _logger = logger;

    public async Task<ServiceResponseDto<OtpVerificationResponseDto>> CreateOtpAsync(CreateOtpRequestDto request, CancellationToken cancellationToken)
    {
        var entity = new Entities.OtpVerification()
        {
            Id = Guid.NewGuid(),
            Code = OtpConstants.OTP_CODE,
            PhoneNumber = request.PhoneNumber.Trim(),
            InsertedDate = DateTime.Now,
            IsActive = (int)ActiveFlag.Active
        };

        await this._repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        this._logger.LogInformation("Create Verification Code:{0}", entity.Code);

        var response = this._mapper.Map<OtpVerificationResponseDto>(entity);

        return ServiceResponseDto<OtpVerificationResponseDto>.SetSuccess(response);
    }

    public ServiceResponseDto VerifyOtp(string code)
    {
        if (code != OtpConstants.OTP_CODE)
            return ServiceResponseDto.SetFail(message: "Doğrulama kodu yanlıştır. Lütfen doğrulama kodunu tekrar giriniz.");

        this._logger.LogInformation("Checked Verification Code:{0}", code);

        return ServiceResponseDto.SetSuccess();
    }
}
