using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.Services.CountryInfo;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

[ApiController]
[Route("api/")]
[Produces("application/json")]
[Authorize]
public class CountryInfoSoapController(ICountryInfoSoapService countryInfoSoapService,
                                       IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICountryInfoSoapService _countryInfoSoapService = countryInfoSoapService;

    [HttpGet("countryName/{isoCode}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCountryName([FromRoute] string isoCode)
    {
        var response = await this._countryInfoSoapService.GetCountryNameAsync(isoCode).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    [HttpGet("countries")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCountries()
    {
        var response = await this._countryInfoSoapService.GetCountriesAsync().ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }
}
