using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Common.Constants;
using SummerSchool.ApplicationSystems.Mvc.Models.Base.Response;
using SummerSchool.ApplicationSystems.Shared.Enums;
using SummerSchool.ApplicationSystems.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[AllowAnonymous]
public class BaseAdminController : Controller
{
    public UserModel UserInfo { get; set; }

    private readonly IHttpClientFactory _httpClientFactory;

    protected BaseAdminController(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;

        if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            ClaimsPrincipal user = httpContextAccessor.HttpContext.User;
            UserInfo = new UserModel();

            var idClaim = user.FindFirst("Id")?.Value;
            if (idClaim != null && Guid.TryParse(idClaim, out var userId))
                UserInfo.Id = userId;

            var userNameClaim = user.FindFirst("UserName")?.Value;
            if (!string.IsNullOrEmpty(userNameClaim))
                UserInfo.UserName = userNameClaim;

            var nameSurnameClaim = user.FindFirst("NameAndSurname")?.Value;
            if (!string.IsNullOrEmpty(nameSurnameClaim))
                UserInfo.NameAndSurname = nameSurnameClaim;

            var phoneNumberClaim = user.FindFirst("PhoneNumber")?.Value;
            if (!string.IsNullOrEmpty(phoneNumberClaim))
                UserInfo.PhoneNumber = phoneNumberClaim;

            var emailClaim = user.FindFirst("EMail")?.Value;
            if (!string.IsNullOrEmpty(emailClaim))
                UserInfo.EMail = emailClaim;

            var accessTokenClaim = user.FindFirst("AccessToken")?.Value;
            if (!string.IsNullOrEmpty(accessTokenClaim))
                UserInfo.AccessToken = accessTokenClaim;

            var expirationClaim = user.FindFirst("Expiration")?.Value;
            if (!string.IsNullOrEmpty(expirationClaim) && DateTime.TryParse(expirationClaim, out var expiration))
                UserInfo.Expiration = expiration;

            var userTypeClaim = user.FindFirst("UserType")?.Value;
            if (!string.IsNullOrEmpty(userTypeClaim) && Enum.TryParse<UserType>(userTypeClaim, out var userType))
                UserInfo.UserType = userType;
        }
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (context.HttpContext.Request.Path.Value != AdminRouteConstants.LOGOUT && this.UserInfo.Expiration < DateTime.Now)
            {
                context.Result = new RedirectResult(AdminRouteConstants.LOGOUT);
                return;
            }

            if (context.HttpContext.Request.Path.Value == AdminRouteConstants.LOGIN && this.UserInfo.Expiration > DateTime.Now)
            {
                context.Result = new RedirectResult("/");
                return;
            }

            if (context.HttpContext.Request.Path.Value != AdminRouteConstants.LOGOUT)
                ViewBag.UserInfo = this.UserInfo;
        }
        else
        {
            if (context.HttpContext.Request.Path.Value == AdminRouteConstants.LOGOUT)
            {
                context.Result = new RedirectResult(AdminRouteConstants.LOGIN);
                return;
            }

        }
    }

    protected async Task<ApiResponse<T>> GetApiRequestAsync<T>(string endPoint) where T : class
    {
        var responseMessage = await this.GetHttpClient().GetAsync(endPoint).ConfigureAwait(false);
        return await HandleApiResponseAsync<ApiResponse<T>>(responseMessage).ConfigureAwait(false);
    }

    protected async Task<ApiResponse<T>> PostApiRequestAsync<T>(string endPoint, object data) where T : class
    {
        var content = CreateJsonContent(data);
        var responseMessage = await this.GetHttpClient().PostAsync(endPoint, content).ConfigureAwait(false);
        return await HandleApiResponseAsync<ApiResponse<T>>(responseMessage).ConfigureAwait(false);
    }

    protected async Task<ApiResponse> PostApiRequestAsync(string endPoint, object data)
    {
        var content = CreateJsonContent(data);
        var responseMessage = await this.GetHttpClient().PostAsync(endPoint, content).ConfigureAwait(false);
        return await HandleApiResponseAsync<ApiResponse>(responseMessage).ConfigureAwait(false);
    }

    protected async Task<ApiResponse> PutApiRequestAsync(string endPoint, object data)
    {
        var content = CreateJsonContent(data);
        var responseMessage = await this.GetHttpClient().PutAsync(endPoint, content).ConfigureAwait(false);
        return await HandleApiResponseAsync<ApiResponse>(responseMessage).ConfigureAwait(false);
    }



    private HttpClient GetHttpClient(IDictionary<string, string> requestHeaders = null)
    {
        var httpClient = this._httpClientFactory.CreateClient(HttpClientNames.API_CLIENT);

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        requestHeaders = requestHeaders ?? new Dictionary<string, string>();

        if (UserInfo?.AccessToken != null)
            requestHeaders.Add("Authorization", $"Bearer {UserInfo.AccessToken}");

        foreach (var header in requestHeaders)
        {
            if (httpClient.DefaultRequestHeaders.Any(x => x.Key == header.Key))
                httpClient.DefaultRequestHeaders.Remove(header.Key);

            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        return httpClient;
    }

    private async Task<T> HandleApiResponseAsync<T>(HttpResponseMessage responseMessage) where T : class
    {
        var json = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

        try
        {
            if (responseMessage.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<T>(json, GetJsonSerializerOptions());
            else
                return CreateFailResponse<T>((int)responseMessage.StatusCode, json);
        }
        catch (Exception)
        {
            return CreateFailResponse<T>((int)responseMessage.StatusCode, json);
        }
    }

    private StringContent CreateJsonContent(object data)
    {
        var json = JsonSerializer.Serialize(data, GetJsonSerializerOptions());
        return new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
    }

    private JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    private T CreateFailResponse<T>(int statusCode, string message) where T : class
    {
        if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(ApiResponse<>))
        {
            var dataType = typeof(T).GetGenericArguments()[0];
            var method = typeof(ApiResponse<>).MakeGenericType(dataType)
                .GetMethod("SetFail", new[] { dataType, typeof(int), typeof(string) });

            return method?.Invoke(null, new object[] { null, statusCode, message }) as T;
        }
        else if (typeof(T) == typeof(ApiResponse))
        {
            var method = typeof(ApiResponse).GetMethod("SetFail", new[] { typeof(int), typeof(string) });
            return method?.Invoke(null, new object[] { statusCode, message }) as T;
        }

        return null;
    }
}
