using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;
using SummerSchool.ApplicationSystems.Core.Enums;
using SummerSchool.ApplicationSystems.Core.Options;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.Course;
using SummerSchool.ApplicationSystems.Core.Services.CourseApplication;
using SummerSchool.ApplicationSystems.Core.Services.Student;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.CourseApplication;

public class CourseApplicationCommandService(IBaseRepository<Entities.CourseApplication> repository,
                                      ICourseQueryService courseQueryService,
                                      IStudentQueryService studentQueryService,
                                      IMapper mapper,
                                      UserOptions userOptions) : BaseService<Entities.CourseApplication>(repository), ICourseApplicationCommandService
{
    private readonly IBaseRepository<Entities.CourseApplication> _repository = repository;
    private readonly ICourseQueryService _courseQueryService = courseQueryService;
    private readonly IStudentQueryService _studentQueryService = studentQueryService;
    private readonly IMapper _mapper = mapper;
    private readonly UserOptions _userOptions = userOptions;

    public async Task<ServiceResponseDto> CreateCourseApplicationAsync(CreateCourseApplicationRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this.ValidateCourseApplicationDataAsync(request, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessful)
            return response;

        var entity = this._mapper.Map<Entities.CourseApplication>(request);
        entity.StudentId = this._userOptions.Id;
        entity.ApplicationStatus = (int)ApplicationStatus.Application;
        entity.IsActive = (int)ActiveFlag.Active;

        await this._repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess();
    }
    public async Task<ServiceResponseDto> UpdateApplicationStatusAsync(UpdateCourseApplicationStatusRequestDto request, CancellationToken cancellationToken)
    {
        var entity = await this._repository.FindAsync(request.Id, cancellationToken).ConfigureAwait(false);
        if (entity == null)
            return ServiceResponseDto.SetFail(message: "Durumu güncellenmek istenen başvuru bulunamadı.");

        entity.ApplicationStatus = request.ApplicationStatus;
        entity.UpdatedUser = this._userOptions.UserName;
        entity.UpdatedDate = DateTime.Now;

        await this._repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess();
    }

    private async Task<ServiceResponseDto> ValidateCourseApplicationDataAsync(CreateCourseApplicationRequestDto request, CancellationToken cancellationToken)
    {
        if (!this._studentQueryService.Exists(x => x.Id == this._userOptions.Id))
            return ServiceResponseDto.SetFail(message: "İşlem yapan öğrenci bulunamadı.");

        var responseCourse = await this._courseQueryService.FindAsync(request.CourseId, cancellationToken).ConfigureAwait(false);
        if (!responseCourse.IsSuccessful)
        {
            return ServiceResponseDto.SetFail(message: "Seçilen ders bulunamadı.");
        }
        else
        {
            var appCount = await this._repository.GetDbSet().CountAsync(x => x.CourseId == request.CourseId && x.ApplicationStatus == (int)ApplicationStatus.Acceptance, cancellationToken).ConfigureAwait(false);
            if (appCount + 1 > responseCourse.Result.Quota)
                return ServiceResponseDto.SetFail(message: "Seçilen dersin kontenjanı dolmuş.");
        }

        if (this._repository.Exists(x => x.StudentId == this._userOptions.Id && x.CourseId == request.CourseId))
            return ServiceResponseDto.SetFail(message: "Başvurulmak istenen derse daha önce başvuru yapılmış.");

        return ServiceResponseDto.SetSuccess();
    }
}
