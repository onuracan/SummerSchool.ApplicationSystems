using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Response;
using SummerSchool.ApplicationSystems.Core.Enums;
using SummerSchool.ApplicationSystems.Core.Options;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.CourseApplication;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using SummerSchool.ApplicationSystems.Shared.Extensions;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.CourseApplication;

public class CourseApplicationQueryService(IBaseRepository<Entities.CourseApplication> repository,
                                           UserOptions userOptions) : BaseService<Entities.CourseApplication>(repository), ICourseApplicationQueryService
{
    private readonly IBaseRepository<Entities.CourseApplication> _repository = repository;
    private readonly UserOptions _userOptions = userOptions;

    public async Task<ServiceResponseDto<IEnumerable<CourseApplicationListResponseDto>>> GetCourseApplicationsByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
    {
        var query = this._repository.GetQueryable(predicate: x => x.CourseId == courseId,
                                                  include: i => i.Include(x => x.Student)
                                                                 .Include(x => x.Course));

        if (!await query.AnyAsync().ConfigureAwait(false))
            return ServiceResponseDto<IEnumerable<CourseApplicationListResponseDto>>.SetFail(null, StatusCodes.Status204NoContent, "Ders başvuruları bulunamadı.");

        var list = await query.Select(x => new CourseApplicationListResponseDto()
        {
            StudentInfo = $"{x.Student.FirstName} {x.Student.LastName}",
            CourseInfo = $"{x.Course.Code} ({x.Course.Name})",
            ApplicationStatusInfo = ((ApplicationStatus)x.ApplicationStatus).GetDescription(),
            ApplicationStatusDescription = x.ApplicationStatusDescription,
            UpdatedUser = x.UpdatedUser,
            UpdatedDate = x.UpdatedDate.Value
        }).ToListAsync(cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto<IEnumerable<CourseApplicationListResponseDto>>.SetSuccess(list);
    }
    public async Task<ServiceResponseDto<IEnumerable<CourseApplicationListResponseDto>>> GetCourseApplicationsByStudentIdAsync(CancellationToken cancellationToken)
    {
        var query = this._repository.GetQueryable(predicate: x => x.StudentId == this._userOptions.Id,
                                                  include: i => i.Include(x => x.Student)
                                                                 .Include(x => x.Course));

        if (!await query.AnyAsync().ConfigureAwait(false))
            return ServiceResponseDto<IEnumerable<CourseApplicationListResponseDto>>.SetFail(null, StatusCodes.Status204NoContent, "Ders başvuruları bulunamadı.");

        var list = await query.Select(x => new CourseApplicationListResponseDto()
        {
            CourseInfo = $"{x.Course.Code} - {x.Course.Name}",
            ApplicationStatusInfo = ((ApplicationStatus)x.ApplicationStatus).GetDescription(),
            ApplicationStatusDescription = x.ApplicationStatusDescription,
        }).ToListAsync(cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto<IEnumerable<CourseApplicationListResponseDto>>.SetSuccess(list);
    }
}
