using AutoMapper;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;
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
        if (!this._studentQueryService.Exists(x => x.Id == request.StudentId))
            return ServiceResponseDto.SetFail(message: "İşlem yapan öğrenci bulunamadı.");

        if (!this._courseQueryService.Exists(x => x.Id == request.CourseId))
            return ServiceResponseDto.SetFail(message: "Seçilen ders bulunamadı.");

        if (this._repository.Exists(x => x.StudentId == request.StudentId && x.CourseId == request.CourseId))
            return ServiceResponseDto.SetFail(message: "Başvurulmak istenen derse daha önce başvuru yapılmış.");

        var entity = this._mapper.Map<Entities.CourseApplication>(request);

        await this._repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess();
    }
    public async Task<ServiceResponseDto> UpdateApplicationStatusAsync(UpdateCourseApplicationStatusRequestDto request, CancellationToken cancellationToken)
    {
        var entity = await this._repository.FindAsync(request.Id, cancellationToken).ConfigureAwait(false);
        if (entity == null)
            return ServiceResponseDto.SetFail(message: "Durumu güncellenmek istenen başvuru bulunamadı.");

        entity.ApplicationStatus = request.ApplicationStatus;
        entity.ApplicationStatusDescription = request.ApplicationStatusDescription;
        entity.UpdatedUser = this._userOptions.UserName;
        entity.UpdatedDate = DateTime.Now;

        await this._repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess();
    }
}
