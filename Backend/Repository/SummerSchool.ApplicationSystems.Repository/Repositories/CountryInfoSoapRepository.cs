using ServiceReference;
using SummerSchool.ApplicationSystems.Core.DTOs.CountryInfo;
using SummerSchool.ApplicationSystems.Core.Repositories.CountryInfo;

namespace SummerSchool.ApplicationSystems.Repository.Repositories;

public class CountryInfoSoapRepository : ICountryInfoRepository
{
    private readonly CountryInfoServiceSoapTypeClient _client;

    public CountryInfoSoapRepository()
    {
        _client = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
    }

    public async Task<string> GetCountryNameAsync(string isoCode)
    {
        var response = await this._client.CountryNameAsync(isoCode);

        return response.Body.CountryNameResult;
    }

    public async Task<IEnumerable<CountryCodeAndNameDto>> GetCountriesAsync()
    {
        var res = await this._client.ListOfCountryNamesByCodeAsync();

        return res.Body.ListOfCountryNamesByCodeResult.Select(x => new CountryCodeAndNameDto()
        {
            CountryCode = x.sISOCode,
            CountryName = x.sName
        }).AsEnumerable();
    }
}
