using AutoMapper;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Request;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.Student;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.Student;

public class StudentCommandService(IBaseRepository<Entities.Student> repository,
                                   IMapper mapper) : BaseService<Entities.Student>(repository), IStudentCommandService
{
    private readonly IBaseRepository<Entities.Student> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResponseDto> CreateStudentAsync(CreateStudentRequestDto request, CancellationToken cancellationToken)
    {
        if (this._repository.Exists(x => x.IdentityNumber == request.IdentityNumber))
            return ServiceResponseDto.SetFail(message: "Kimlik/Pasaport numarası daha önce kaydedilmiş.");

        if (!string.IsNullOrEmpty(request.SchoolNumber) && this._repository.Exists(x => x.SchoolNumber == request.SchoolNumber))
            return ServiceResponseDto.SetFail(message: "Okul numarası daha önce kaydedilmiş.");

        if (this._repository.Exists(x => x.PhoneNumber == request.PhoneNumber))
            return ServiceResponseDto.SetFail(message: "Telefon numarası daha önce kaydedilmiş.");

        if (this._repository.Exists(x => x.EMail == request.EMail))
            return ServiceResponseDto.SetFail(message: "E-Posta daha önce kaydedilmiş.");

        var entity = this._mapper.Map<Entities.Student>(request);

        await this._repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess();
    }
    public async Task<ServiceResponseDto> UpdateStudentAsync(UpdateStudentRequestDto request, CancellationToken cancellationToken)
    {
        if (this._repository.Exists(x => x.Id != request.Id && x.IdentityNumber == request.IdentityNumber))
            return ServiceResponseDto.SetFail(message: "Kimlik/Pasaport numarası daha önce kaydedilmiş.");

        if (!string.IsNullOrEmpty(request.SchoolNumber) && this._repository.Exists(x => x.Id != request.Id && x.SchoolNumber == request.SchoolNumber))
            return ServiceResponseDto.SetFail(message: "Okul numarası daha önce kaydedilmiş.");

        if (this._repository.Exists(x => x.Id != request.Id && x.PhoneNumber == request.PhoneNumber))
            return ServiceResponseDto.SetFail(message: "Telefon numarası daha önce kaydedilmiş.");

        if (this._repository.Exists(x => x.Id != request.Id && x.EMail == request.EMail))
            return ServiceResponseDto.SetFail(message: "E-Posta daha önce kaydedilmiş.");

        var entity = await this._repository.FindAsync(request.Id, cancellationToken).ConfigureAwait(false);
        if (entity == null)
            return ServiceResponseDto.SetFail(message: "Güncellenecek öğrenci bulunamadı.");

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.IdentityNumber = request.IdentityNumber;
        entity.SchoolNumber = request.SchoolNumber;
        entity.Department = request.Department;
        entity.Faculty = request.Faculty;
        entity.PhoneNumber = request.PhoneNumber;
        entity.EMail = request.EMail;
        entity.CountryId = request.CountryId;

        await this._repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess();

    }
}
