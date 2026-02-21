using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;
using SummerSchool.ApplicationSystems.Core.Services.Base;

namespace SummerSchool.ApplicationSystems.Core.Services.CourseApplication;

public interface ICourseApplicationCommandService : IBaseService<Entities.CourseApplication>
{
    Task<ServiceResponseDto> CreateCourseApplicationAsync(CreateCourseApplicationRequestDto request, CancellationToken cancellationToken);
    Task<ServiceResponseDto> UpdateApplicationStatusAsync(UpdateCourseApplicationStatusRequestDto request, CancellationToken cancellationToken);
}
