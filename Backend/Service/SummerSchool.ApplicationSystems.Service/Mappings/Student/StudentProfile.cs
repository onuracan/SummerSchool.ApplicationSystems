using AutoMapper;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Request;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Response;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Mappings.Student;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Entities.Student, StudentResponseDto>();
        CreateMap<CreateStudentRequestDto, Entities.Student>()
         .ForMember(dest => dest.Id, opt => opt.Ignore())
         .AfterMap((src, dest) => dest.Id = Guid.NewGuid());
        CreateMap<UpdateStudentRequestDto, Entities.Student>();
    }
}
