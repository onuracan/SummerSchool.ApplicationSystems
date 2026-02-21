using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Response;
using SummerSchool.ApplicationSystems.Core.Services.Base;

namespace SummerSchool.ApplicationSystems.Core.Services.CourseApplication;

public interface ICourseApplicationQueryService : IBaseService<Entities.CourseApplication>
{
    Task<ServiceResponseDto<IEnumerable<CourseApplicationListResponseDto>>> GetCourseApplicationsByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
    Task<ServiceResponseDto<IEnumerable<CourseApplicationListResponseDto>>> GetCourseApplicationsByStudentIdAsync(CancellationToken cancellationToken);
}
