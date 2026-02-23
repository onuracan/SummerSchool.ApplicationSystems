using Microsoft.AspNetCore.Http;
using SummerSchool.ApplicationSystems.Core.Constants;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.CountryInfo;
using SummerSchool.ApplicationSystems.Core.Repositories.CountryInfo;
using SummerSchool.ApplicationSystems.Core.Services.CountryInfo;

namespace SummerSchool.ApplicationSystems.Service.Services.CountryInfo;

public class CountryInfoSoapService(ICountryInfoRepository repository) : ICountryInfoSoapService
{
    private readonly ICountryInfoRepository _repository = repository;

    public async Task<ServiceResponseDto<string>> GetCountryNameAsync(string isoCode)
    {
        var data = await this._repository.GetCountryNameAsync(isoCode);

        if (data == SoapConstants.COUNTRY_NOT_FOUND_MESSAGE)
            return ServiceResponseDto<string>.SetFail(null, StatusCodes.Status204NoContent, "Seçilen koda ait ülke bulunamadı.");

        return ServiceResponseDto<string>.SetSuccess(data);
    }

    public async Task<ServiceResponseDto<IEnumerable<CountryCodeAndNameDto>>> GetCountriesAsync()
    {
        var data = await this._repository.GetCountriesAsync();

        if (data == null || !data.Any())
            return ServiceResponseDto<IEnumerable<CountryCodeAndNameDto>>.SetFail(null, StatusCodes.Status204NoContent, "Ülke listesi alınamadı.");

        return ServiceResponseDto<IEnumerable<CountryCodeAndNameDto>>.SetSuccess(data);
    }
}
