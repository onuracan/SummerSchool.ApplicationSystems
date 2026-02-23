using AutoMapper;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Request;
using SummerSchool.ApplicationSystems.Core.Enums;
using SummerSchool.ApplicationSystems.Core.Repositories.Base;
using SummerSchool.ApplicationSystems.Core.Services.CountryInfo;
using SummerSchool.ApplicationSystems.Core.Services.Student;
using SummerSchool.ApplicationSystems.Service.Services.Base;
using Entities = SummerSchool.ApplicationSystems.Core.Entities;

namespace SummerSchool.ApplicationSystems.Service.Services.Student;

public class StudentCommandService(IBaseRepository<Entities.Student> repository,
                                   ICountryInfoSoapService countryInfoSoapService,
                                   IMapper mapper) : BaseService<Entities.Student>(repository), IStudentCommandService
{
    private readonly IBaseRepository<Entities.Student> _repository = repository;
    private readonly ICountryInfoSoapService _countryInfoSoapService = countryInfoSoapService;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResponseDto> CreateStudentAsync(CreateStudentRequestDto request, CancellationToken cancellationToken)
    {
        var response = await ValidateStudentDataAsync(
               identityNumber: request.IdentityNumber,
               schoolNumber: request.SchoolNumber,
               phoneNumber: request.PhoneNumber,
               email: request.EMail,
               countryCode: request.CountryCode,
               excludeId: null,
               cancellationToken: cancellationToken
           ).ConfigureAwait(false);

        if (!response.IsSuccessful)
            return response;

        var entity = this._mapper.Map<Entities.Student>(request);
        entity.IsActive = (int)ActiveFlag.Active;

        await this._repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess();
    }
    public async Task<ServiceResponseDto> UpdateStudentAsync(UpdateStudentRequestDto request, CancellationToken cancellationToken)
    {
        var response = await ValidateStudentDataAsync(
           identityNumber: request.IdentityNumber,
           schoolNumber: request.SchoolNumber,
           phoneNumber: request.PhoneNumber,
           email: request.EMail,
           countryCode: request.CountryCode,
           excludeId: request.Id,
           cancellationToken: cancellationToken
       ).ConfigureAwait(false);

        if (!response.IsSuccessful)
            return response;

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
        entity.CountryCode = request.CountryCode;

        await this._repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);

        return ServiceResponseDto.SetSuccess();

    }

    private async Task<ServiceResponseDto> ValidateStudentDataAsync(
        string identityNumber,
        string? schoolNumber,
        string phoneNumber,
        string email,
        string countryCode,
        Guid? excludeId,
        CancellationToken cancellationToken)
    {
        if (this._repository.Exists(x => x.IdentityNumber == identityNumber && (excludeId == null || x.Id != excludeId)))
            return ServiceResponseDto.SetFail(message: "Kimlik/Pasaport numarası daha önce kaydedilmiş.");

        if (!string.IsNullOrEmpty(schoolNumber) &&
            this._repository.Exists(x => x.SchoolNumber == schoolNumber && (excludeId == null || x.Id != excludeId)))
            return ServiceResponseDto.SetFail(message: "Okul numarası daha önce kaydedilmiş.");
        
        if (this._repository.Exists(x => x.PhoneNumber == phoneNumber && (excludeId == null || x.Id != excludeId)))
            return ServiceResponseDto.SetFail(message: "Telefon numarası daha önce kaydedilmiş.");

        if (this._repository.Exists(x => x.EMail == email && (excludeId == null || x.Id != excludeId)))
            return ServiceResponseDto.SetFail(message: "E-Posta daha önce kaydedilmiş.");

        var countryResponse = await this._countryInfoSoapService.GetCountryNameAsync(countryCode).ConfigureAwait(false);
        if (!countryResponse.IsSuccessful)
            return ServiceResponseDto.SetFail(message: countryResponse.Message);

        return ServiceResponseDto.SetSuccess();
    }
}
