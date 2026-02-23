using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Request;
using SummerSchool.ApplicationSystems.Core.Services.Base;

namespace SummerSchool.ApplicationSystems.Core.Services.Course;

public interface ICourseCommandService : IBaseService<Entities.Course>
{
    Task<ServiceResponseDto> CreateCourseAsync(CreateCourseRequestDto request, CancellationToken cancellationToken);
    Task<ServiceResponseDto> UpdateCourseAsync(UpdateCourseRequestDto request, CancellationToken cancellationToken);
}
