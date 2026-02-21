using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.CountryInfo;

namespace SummerSchool.ApplicationSystems.Core.Services.CountryInfo;

public interface ICountryInfoSoapService
{
    Task<ServiceResponseDto<string>> GetCountryNameAsync(string isoCode);
    Task<ServiceResponseDto<IEnumerable<CountryCodeAndNameDto>>> GetCountriesAsync();
}
