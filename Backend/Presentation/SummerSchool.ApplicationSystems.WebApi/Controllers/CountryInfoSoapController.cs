using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.Services.CountryInfo;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

/// <summary>
/// SOAP Web Service entegrasyonu - Ülke bilgileri (Bonus Özellik)
/// </summary>
/// <remarks>
/// Bu controller SOAP web servisinden ülke bilgilerini çeker.
/// WSDL: http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso?WSDL
/// </remarks>
[ApiController]
[Route("api/")]
[Produces("application/json")]
[Authorize]
public class CountryInfoSoapController(ICountryInfoSoapService countryInfoSoapService,
                                       IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICountryInfoSoapService _countryInfoSoapService = countryInfoSoapService;

    /// <summary>
    /// ISO koda göre ülke adını getirir
    /// </summary>
    /// <param name="isoCode">ISO ülke kodu (örn: TR, US, DE)</param>
    /// <returns>Ülke adı</returns>
    /// <remarks>
    /// SOAP web servisinden ülke adını çeker.
    /// 
    /// Örnek kullanım:
    /// 
    ///     GET /api/countryName/TR
    ///     Response: "Turkey"
    /// 
    /// </remarks>
    /// <response code="200">Ülke adı başarıyla döndürüldü</response>
    /// <response code="204">Ülke bulunamadı</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpGet("countryName/{isoCode}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCountryName([FromRoute] string isoCode)
    {
        var response = await this._countryInfoSoapService.GetCountryNameAsync(isoCode).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Tüm ülkelerin listesini getirir
    /// </summary>
    /// <returns>Ülke listesi (ISO kod ve ad)</returns>
    /// <remarks>
    /// SOAP web servisinden tüm ülkeleri çeker.
    /// 
    /// Örnek response:
    /// 
    ///     [
    ///         { "isoCode": "TR", "name": "Turkey" },
    ///         { "isoCode": "US", "name": "United States" }
    ///     ]
    /// 
    /// </remarks>
    /// <response code="200">Ülke listesi başarıyla döndürüldü</response>
    /// <response code="204">Ülke bulunamadı</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpGet("countries")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCountries()
    {
        var response = await this._countryInfoSoapService.GetCountriesAsync().ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }
}
