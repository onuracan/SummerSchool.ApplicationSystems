using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.Course.Request;
using SummerSchool.ApplicationSystems.Core.Services.Course;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

/// <summary>
/// Ders yönetim işlemlerini yöneten controller
/// </summary>
[ApiController]
[Route("api/")]
[Produces("application/json")]
[Authorize]
public class CourseController(ICourseQueryService courseQueryService,
                              ICourseCommandService courseCommandService,
                              IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICourseQueryService _courseQueryService = courseQueryService;
    private readonly ICourseCommandService _courseCommandService = courseCommandService;

    /// <summary>
    /// Tüm yaz okulu derslerini listeler
    /// </summary>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Ders listesi</returns>
    /// <remarks>
    /// Kontenjan bilgisi ve başvuru sayısı ile birlikte döner.
    /// 
    /// Örnek response:
    /// 
    ///     [
    ///         {
    ///             "id": "guid",
    ///             "code": "BİL101",
    ///             "name": "Programlama Temelleri",
    ///             "department": "Bilgisayar Mühendisliği",
    ///             "faculty": "Mühendislik Fakültesi",
    ///             "quota": 30,
    ///             "applicationCount": 25
    ///         }
    ///     ]
    /// 
    /// </remarks>
    /// <response code="200">Ders listesi başarıyla döndürüldü</response>
    /// <response code="401">Yetkisiz erişim - Token gerekli</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpGet("courses")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCourses(CancellationToken cancellationToken)
    {
        var response = await this._courseQueryService.GetCoursesAsync(cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Dropdown listesi için ders listesi döner
    /// </summary>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Sadece ID ve isim içeren ders listesi</returns>
    /// <response code="200">Ders listesi başarıyla döndürüldü</response>
    /// <response code="401">Yetkisiz erişim - Token gerekli</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpGet("courseDropdownList")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCourseDropdownList(CancellationToken cancellationToken)
    {
        var response = await this._courseQueryService.GetCourseDropdownListAsync(cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Yeni ders ekler (Sadece Admin)
    /// </summary>
    /// <param name="request">Ders bilgileri</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Oluşturma sonucu</returns>
    /// <remarks>
    /// Örnek istek:
    /// 
    ///     POST /api/courses
    ///     {
    ///         "code": "BİL101",
    ///         "name": "Programlama Temelleri",
    ///         "department": "Bilgisayar Mühendisliği",
    ///         "faculty": "Mühendislik Fakültesi",
    ///         "quota": 30
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Ders başarıyla oluşturuldu</response>
    /// <response code="400">Geçersiz istek</response>
    /// <response code="401">Yetkisiz erişim</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPost("courses")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._courseCommandService.CreateCourseAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Mevcut dersi günceller (Sadece Admin)
    /// </summary>
    /// <param name="id">Ders ID</param>
    /// <param name="request">Güncellenmiş ders bilgileri</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Güncelleme sonucu</returns>
    /// <response code="200">Ders başarıyla güncellendi</response>
    /// <response code="400">Ders bulunamadı</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPut("courses/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateCourse([FromRoute] Guid id, [FromBody] UpdateCourseRequestDto request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await this._courseCommandService.UpdateCourseAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }
}
