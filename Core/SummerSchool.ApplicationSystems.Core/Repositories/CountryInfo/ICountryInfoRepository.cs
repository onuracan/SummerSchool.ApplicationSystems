using SummerSchool.ApplicationSystems.Core.DTOs.CountryInfo;

namespace SummerSchool.ApplicationSystems.Core.Repositories.CountryInfo;

public interface ICountryInfoRepository
{
    Task<string> GetCountryNameAsync(string isoCode);
    Task<IEnumerable<CountryCodeAndNameDto>> GetCountriesAsync();
}
