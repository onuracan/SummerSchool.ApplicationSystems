using AutoMapper;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Request;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Mappings.Course;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CreateCourseRequestDto, Entities.Course>()
         .ForMember(dest => dest.Id, opt => opt.Ignore())
         .AfterMap((src, dest) => dest.Id = Guid.NewGuid());
        CreateMap<UpdateCourseRequestDto, Entities.Course>();
    }
}
