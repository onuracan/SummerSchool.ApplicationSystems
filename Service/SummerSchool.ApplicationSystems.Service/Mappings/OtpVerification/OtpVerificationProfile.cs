using AutoMapper;
using SummerSchool.ApplicationSystems.Core.DTOs.OtpVerification.Response;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Mappings.OtpVerification;

public class OtpVerificationProfile : Profile
{
    public OtpVerificationProfile()
    {
        CreateMap<Entities.OtpVerification, OtpVerificationResponseDto>();
    }
}
