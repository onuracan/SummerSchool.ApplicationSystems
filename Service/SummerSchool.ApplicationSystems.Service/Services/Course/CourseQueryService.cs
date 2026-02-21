using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Response;
using SummerSchool.ApplicationSystems.Core.Options;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.Course;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.Course;

public class CourseQueryService(IBaseRepository<Entities.Course> repository,
                                UserOptions userOptions) : BaseService<Entities.Course>(repository), ICourseQueryService
{
    private readonly IBaseRepository<Entities.Course> _repository = repository;
    private readonly UserOptions _userOptions = userOptions;

    public async Task<ServiceResponseDto<IEnumerable<CourseListResponseDto>>> GetCoursesAsync(CancellationToken cancellationToken)
    {
        var query = this._repository.GetQueryable(include: i => i.Include(x => x.CourseApplications));

        if (!await query.AnyAsync().ConfigureAwait(false))
            return ServiceResponseDto<IEnumerable<CourseListResponseDto>>.SetFail(null, StatusCodes.Status204NoContent, "Dersler bulunamadı.");

        var list = await query.Select(x => new CourseListResponseDto()
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            Department = x.Department,
            Faculty = x.Faculty,
            Quota = x.Quota,
            ApplicationCount = x.CourseApplications.Count,
            CanBeApply = x.CourseApplications.Any(x => x.StudentId == this._userOptions.Id)
        }).ToListAsync(cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto<IEnumerable<CourseListResponseDto>>.SetSuccess(list);
    }
}
