using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Response;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.Course;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.Course;

public class CourseQueryService(IBaseRepository<Entities.Course> repository) : BaseService<Entities.Course>(repository), ICourseQueryService
{
    private readonly IBaseRepository<Entities.Course> _repository = repository;

    public async Task<ServiceResponseDto<IEnumerable<CourseListResponseDto>>> GetCoursesAsync(CancellationToken cancellationToken)
    {
        var query = this._repository.GetQueryable();

        if (!await query.AnyAsync().ConfigureAwait(false))
            return ServiceResponseDto<IEnumerable<CourseListResponseDto>>.SetFail(null, StatusCodes.Status204NoContent, "Dersler bulunamadı.");

        var list = await query.Select(x => new CourseListResponseDto()
        {
            Code = x.Code,
            Name = x.Name,
            Department = x.Department,
            Faculty = x.Faculty,
            Quota = x.Quota
        }).ToListAsync(cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto<IEnumerable<CourseListResponseDto>>.SetSuccess(list);
    }
}
