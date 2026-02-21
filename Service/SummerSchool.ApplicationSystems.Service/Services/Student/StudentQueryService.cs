using AutoMapper;
using Microsoft.AspNetCore.Http;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Response;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.Student;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.Student;

public class StudentQueryService(IBaseRepository<Entities.Student> repository,
                                 IMapper mapper) : BaseService<Entities.Student>(repository), IStudentQueryService
{
    private readonly IBaseRepository<Entities.Student> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResponseDto<StudentResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await this._repository.FindAsync(id, cancellationToken).ConfigureAwait(false);
        if (entity == null)
            return ServiceResponseDto<StudentResponseDto>.SetFail(null, StatusCodes.Status204NoContent, "Öğrenci bulunamadı.");

        var dto = this._mapper.Map<StudentResponseDto>(entity);

        return ServiceResponseDto<StudentResponseDto>.SetSuccess(dto);
    }
}
