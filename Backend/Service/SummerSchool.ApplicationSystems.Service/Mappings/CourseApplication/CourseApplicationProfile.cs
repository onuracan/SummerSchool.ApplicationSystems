using AutoMapper;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Mappings.CourseApplication;

public class CourseApplicationProfile : Profile
{
    public CourseApplicationProfile()
    {
        CreateMap<CreateCourseApplicationRequestDto, Entities.CourseApplication>()
         .ForMember(dest => dest.Id, opt => opt.Ignore())
         .AfterMap((src, dest) => dest.Id = Guid.NewGuid());
    }
}
