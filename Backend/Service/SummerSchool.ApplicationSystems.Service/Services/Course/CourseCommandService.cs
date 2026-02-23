using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Request;
using SummerSchool.ApplicationSystems.Core.Enums;
using SummerSchool.ApplicationSystems.Core.Options;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.Course;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.Course;

public class CourseCommandService(IBaseRepository<Entities.Course> repository,
                                  IMapper mapper,
                                  UserOptions userOptions) : BaseService<Entities.Course>(repository), ICourseCommandService
{
    private readonly IBaseRepository<Entities.Course> _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly UserOptions _userOptions = userOptions;

    public async Task<ServiceResponseDto> CreateCourseAsync(CreateCourseRequestDto request, CancellationToken cancellationToken)
    {
        if (this._repository.Exists(x => x.Code == request.Code))
            return ServiceResponseDto.SetFail(message: "Kaydedilmek istenen kurs kodu daha önce kayıt edilmiş.");

        if (request.Quota <= 0)
            return ServiceResponseDto.SetFail(message: "Kontenjan 0'dan büyük olmalıdır.");

        var entity = this._mapper.Map<Entities.Course>(request);
        entity.InsertedUser = this._userOptions.UserName;
        entity.InsertedDate = DateTime.Now;
        entity.IsActive = (int)ActiveFlag.Active;

        await this._repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess(message: "Kurs başarıyla kaydedildi.");
    }
    public async Task<ServiceResponseDto> UpdateCourseAsync(UpdateCourseRequestDto request, CancellationToken cancellationToken)
    {
        if (this._repository.Exists(x => x.Code == request.Code && x.Id != request.Id))
            return ServiceResponseDto.SetFail(message: "Güncellenmek istenen kurs kodu daha önce kayıt edilmiş.");

        var entity = await this._repository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.Id,
                                                                   include: i => i.Include(x => x.CourseApplications),
                                                                   cancellationToken: cancellationToken).ConfigureAwait(false);
        if (entity == null)
            return ServiceResponseDto.SetFail(message: "Güncellenmek istenen kurs bulunamadı.");

        if (request.Quota <= 0)
            return ServiceResponseDto.SetFail(message: "Kontenjan 0'dan büyük olmalıdır.");
        else if (entity.CourseApplications.Count > 0 && request.Quota > entity.CourseApplications.Count)
            return ServiceResponseDto.SetFail(message: $"{entity.Code} - {entity.Name} derse ait başvuru bulunmaktadır. Mevcut başvuru sayısı talep edilen kontenjan sayısından büyük olamaz. Başvuru sayısı : {entity.CourseApplications.Count}, Talep edilen kontenjan sayısı: {entity.Quota}");

        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.Department = request.Department;
        entity.Faculty = request.Faculty;
        entity.Quota = request.Quota;
        entity.UpdatedUser = this._userOptions.UserName;
        entity.UpdatedDate = DateTime.Now;

        await this._repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess(message: "Kurs başarıyla kaydedildi.");
    }
}
