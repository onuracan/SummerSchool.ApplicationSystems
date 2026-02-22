using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Response;
using SummerSchool.ApplicationSystems.Core.Services.Base;

namespace SummerSchool.ApplicationSystems.Core.Services.Course;

public interface ICourseQueryService : IBaseService<Entities.Course>
{
    Task<ServiceResponseDto<IEnumerable<CourseListResponseDto>>> GetCoursesAsync(CancellationToken cancellationToken);
    Task<ServiceResponseDto<IEnumerable<CourseDropdownListResponseDto>>> GetCourseDropdownListAsync(CancellationToken cancellationToken);
}
